using Microsoft.AspNetCore.Mvc;
using Pro219.API.DTOs;
using Pro219.API.Utilities;
using Pro219.DAL.Models;
using Pro219.DAL.Repository;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Net.payOS.Types;
using Net.payOS;

namespace Pro219.API.Controllers
{
    [Route("Order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        OrderRepository orderRepository;

        public OrderController()
        {
            orderRepository = new OrderRepository();
        }

       [HttpPost("GetCheckoutUrl")]
       public async Task<ActionResult<string>> GetCheckoutUrl([FromBody] List<CheckoutItemDTO> listProduct, string discountCode = null)
       {

         


            PayOS payOS = new PayOS("09b8a42b-6105-4cd4-a4ee-8492e42e909c", "15cfbaf8-79a4-48a0-908f-248c30538001", "00b20c6b94e21bf27e6cb0ae2f26515637c93d70b2eeb832e7b51e299cba433d");
            List<ItemData> items = new List<ItemData>();
            foreach (var product in listProduct)
            {
                ItemData item = new ItemData(product.ProductName, product.Quantity, (int)product.UnitPrice);
                items.Add(item);
            }

            decimal totalPrice = listProduct.Sum(p => p.UnitPrice * p.Quantity);
            decimal discountAmount = 0;
            if (discountCode != null)
            {
                DiscountCodeRepository discountRepository = new DiscountCodeRepository();
                var discount = await discountRepository.GetDiscountCodeByCode(discountCode);
                if (discount != null)
                {

                    if (discount.DiscountType == "Percentage")
                    {
                        discountAmount = totalPrice * (discount.Value / 100);
                        totalPrice -= discountAmount;
                        discountAmount = discountAmount;
                    }
                    else if (discount.DiscountType == "FixedAmount")
                    {
                        discountAmount = discount.Value;
                        totalPrice -= discountAmount;
                        discountAmount = discountAmount;
                    }
                }
            }
            decimal finalAmount = totalPrice - discountAmount;
            int ordCode = new Random().Next(1, int.MaxValue);
            Order order = new Order();
            try
            {
                order.OrderCode = "DH" + ordCode.ToString();
                order.TotalAmount = finalAmount;
                order.DiscountAmount = discountAmount;
                order.FinalAmount = finalAmount;
                order.PaymentStatus = "Pending";
                order.OrderStatus = "Pending";
                order.Notes = "Đang chờ thanh toán qua PayOS";
                order.CreateAt = DateTime.Now;
                order.LastUpdate = DateTime.Now;
                order.UpdateBy = "System";
                order.Status = 1;
                order.CustomerId = int.Parse(User.FindFirst(ClaimTypes.SerialNumber)?.Value);
                order.ShippingAddressId = 1;
                order.DiscountId = null;
                order.PaymentMethodId = 1;
                var result = await orderRepository.AddOrder(order);


                if (result == null)
                {
                    return BadRequest();
                }
                else
                {
                    OrderItemRepository orderItemRepository = new OrderItemRepository();
                    foreach (var product in listProduct)
                    {
                        OrderItem orderItem = new OrderItem();
                        orderItem.OrderId = result.OrderId;
                        orderItem.ProductVariantId = product.ProductVariantId;
                        orderItem.Quantity = product.Quantity;
                        orderItem.UnitPrice = product.UnitPrice;
                        orderItem.Subtotal = product.UnitPrice * product.Quantity;
                        orderItem.Delete = false;
                        var resultItem = await orderItemRepository.AddOrderItem(orderItem);
                        if(resultItem == null)
                        {
                            return BadRequest();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
            PaymentData paymentData = new PaymentData(ordCode, (int)finalAmount, "Adam Store Thanh toán", items, "https://localhost:7179/Order/PaymentCanceled?orderId=" + order.OrderId + "&errorMessage=" + "Đã hủy thanh toán", "https://localhost:7179/Order/PaymentSuccess?orderId=" + order.OrderId );

            CreatePaymentResult createPayment = await payOS.createPaymentLink(paymentData);
            
            if(createPayment.status == "PENDING")
            {    
              
                return Ok(createPayment.checkoutUrl);
            }
            else
            {
                return BadRequest();
            }           
           

        }

        [HttpGet("PaymentSuccess")]
        public async Task<ActionResult<Order>> PaymentSuccess( [FromQuery] int orderId, [FromQuery] string errorMessage = null)
        {

            try
            {
                var order = await orderRepository.GetOrderById(orderId);
                if(order == null)
                {
                    return NotFound();
                }
                order.PaymentStatus = "Đã thanh toán";
                order.OrderStatus = "Đã thanh toán";
                order.LastUpdate = DateTime.Now;
                order.UpdateBy = "System";
                var result = await orderRepository.UpdateOrder(order);
                if(result == null)
                {
                    return BadRequest();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }

        }

        [HttpGet("PaymentCanceled")]
        public async Task<ActionResult<Order>> PaymentCanceled([FromQuery] int orderId, [FromQuery] string errorMessage = null)
        {
            try
            {
                var order = await orderRepository.GetOrderById(orderId);
                if(order == null)
                {
                    return NotFound();
                }
                order.PaymentStatus = "Canceled";
                order.OrderStatus = "Đã hủy";
                order.LastUpdate = DateTime.Now;
                order.UpdateBy = "System";
                var result = await orderRepository.UpdateOrder(order);
                if(result == null)
                {
                    return BadRequest();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }


      
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            try
            {
                var result = await orderRepository.GetAllOrders();
                if (result == null)
                {
                    return Ok(new List<Order>());
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            try
            {
                var result = await orderRepository.GetOrderById(id);
                if (result == null)
                {
                    return NotFound(Constant.ErrorCode.DataNotFound);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpGet("GetByOrderCode/{orderCode}")]
        public async Task<ActionResult<Order>> GetOrderByOrderCode(string orderCode)
        {
            try
            {
                var result = await orderRepository.GetOrderByOrderCode(orderCode);
                if (result == null)
                {
                    return NotFound(Constant.ErrorCode.DataNotFound);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpGet("GetByCustomerId/{customerId}")]
        public async Task<ActionResult<List<Order>>> GetOrdersByCustomerId(int customerId)
        {
            try
            {
                var result = await orderRepository.GetOrdersByCustomerId(customerId);
                if (result == null)
                {
                    return Ok(new List<Order>());
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<Order>> AddOrder([FromBody] Order order)
        {
            try
            {
                if (order == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var result = await orderRepository.AddOrder(order);
                if (result == null)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult<Order>> UpdateOrder([FromBody] Order order)
        {
            try
            {
                if (order == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                order.UpdateBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var result = await orderRepository.UpdateOrder(order);
                if (result == null)
                {
                    return NotFound(Constant.ErrorCode.DataNotFound);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            try
            {
                var updateBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await orderRepository.DeleteOrder(id, updateBy);
                if (result == null)
                {
                    return NotFound(Constant.ErrorCode.DataNotFound);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpGet("GetInvoice/{id}")]
        public async Task<ActionResult<InvoiceDTO>> GetInvoice(int id)
        {
            try
            {
                var order = await orderRepository.GetOrderByIdForInvoice(id);
                if (order == null)
                {
                    return NotFound(Constant.ErrorCode.DataNotFound);
                }

                var invoice = new InvoiceDTO
                {
                    OrderId = order.OrderId,
                    OrderCode = order.OrderCode,
                    OrderDate = order.OrderDate,
                    OrderStatus = order.OrderStatus,
                    PaymentStatus = order.PaymentStatus,
                    Notes = order.Notes,
                    TotalAmount = order.TotalAmount,
                    DiscountAmount = order.DiscountAmount,
                    FinalAmount = order.FinalAmount
                };

                if (order.Customer != null)
                {
                    invoice.CustomerFullName = order.Customer.FullName;
                    invoice.CustomerPhone = order.Customer.PhoneNumber;
                    invoice.CustomerEmail = order.Customer.Email;
                }

                if (order.ShippingAddressId > 0 && order.ShippingAddress != null)
                {
                    invoice.ShippingAddress = new ShippingAddressDTO
                    {
                        FullName = order.ShippingAddress.FullName,
                        Phone = order.ShippingAddress.Phone,
                        Street = order.ShippingAddress.Street,
                        City = order.ShippingAddress.City,
                        District = order.ShippingAddress.District,
                        OtherInfo = order.ShippingAddress.OtherInfo
                    };
                }

                if (order.DiscountId.HasValue && order.DiscountCode != null)
                {
                    invoice.DiscountInfo = new DiscountInfoDTO
                    {
                        Code = order.DiscountCode.Code,
                        DiscountType = order.DiscountCode.DiscountType,
                        Value = order.DiscountCode.Value,
                        DiscountAmount = order.DiscountAmount
                    };
                }

                if (order.PaymentMethodId.HasValue && order.PaymentMethod != null)
                {
                    invoice.PaymentMethod = new PaymentMethodInfoDTO
                    {
                        Name = order.PaymentMethod.Name,
                        Description = order.PaymentMethod.Description
                    };
                }

                if (order.OrderItems != null && order.OrderItems.Any())
                {
                    foreach (var orderItem in order.OrderItems.Where(oi => oi.Delete != true))
                    {
                        var invoiceItem = new InvoiceItemDTO
                        {
                            OrderItemId = orderItem.OrderItemId,
                            UnitPrice = orderItem.UnitPrice,
                            Quantity = orderItem.Quantity,
                            Subtotal = orderItem.Subtotal
                        };

                        if (orderItem.ProductVariant != null)
                        {
                            if (orderItem.ProductVariant.Product != null)
                            {
                                invoiceItem.ProductName = orderItem.ProductVariant.Product.Name;
                                
                                if (orderItem.ProductVariant.Product.Brand != null)
                                {
                                    invoiceItem.BrandName = orderItem.ProductVariant.Product.Brand.Name;
                                }
                            }

                            if (orderItem.ProductVariant.Size != null)
                            {
                                invoiceItem.SizeName = orderItem.ProductVariant.Size.Name;
                            }

                            if (orderItem.ProductVariant.Color != null)
                            {
                                invoiceItem.ColorName = orderItem.ProductVariant.Color.Name;
                            }
                        }

                        invoice.OrderItems.Add(invoiceItem);
                    }
                }

                return Ok(invoice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpGet("GetInvoicePdf/{id}")]
        public async Task<IActionResult> GetInvoicePdf(int id)
        {
            try
            {
                var order = await orderRepository.GetOrderByIdForInvoice(id);
                if (order == null)
                {
                    return NotFound(Constant.ErrorCode.DataNotFound);
                }

                var invoice = new InvoiceDTO
                {
                    OrderId = order.OrderId,
                    OrderCode = order.OrderCode,
                    OrderDate = order.OrderDate,
                    OrderStatus = order.OrderStatus,
                    PaymentStatus = order.PaymentStatus,
                    Notes = order.Notes,
                    TotalAmount = order.TotalAmount,
                    DiscountAmount = order.DiscountAmount,
                    FinalAmount = order.FinalAmount
                };

                if (order.Customer != null)
                {
                    invoice.CustomerFullName = order.Customer.FullName;
                    invoice.CustomerPhone = order.Customer.PhoneNumber;
                    invoice.CustomerEmail = order.Customer.Email;
                }

                if (order.ShippingAddressId > 0 && order.ShippingAddress != null)
                {
                    invoice.ShippingAddress = new ShippingAddressDTO
                    {
                        FullName = order.ShippingAddress.FullName,
                        Phone = order.ShippingAddress.Phone,
                        Street = order.ShippingAddress.Street,
                        City = order.ShippingAddress.City,
                        District = order.ShippingAddress.District,
                        OtherInfo = order.ShippingAddress.OtherInfo
                    };
                }

                if (order.DiscountId.HasValue && order.DiscountCode != null)
                {
                    invoice.DiscountInfo = new DiscountInfoDTO
                    {
                        Code = order.DiscountCode.Code,
                        DiscountType = order.DiscountCode.DiscountType,
                        Value = order.DiscountCode.Value,
                        DiscountAmount = order.DiscountAmount
                    };
                }

                if (order.PaymentMethodId.HasValue && order.PaymentMethod != null)
                {
                    invoice.PaymentMethod = new PaymentMethodInfoDTO
                    {
                        Name = order.PaymentMethod.Name,
                        Description = order.PaymentMethod.Description
                    };
                }

                if (order.OrderItems != null && order.OrderItems.Any())
                {
                    foreach (var orderItem in order.OrderItems.Where(oi => oi.Delete != true))
                    {
                        var invoiceItem = new InvoiceItemDTO
                        {
                            OrderItemId = orderItem.OrderItemId,
                            UnitPrice = orderItem.UnitPrice,
                            Quantity = orderItem.Quantity,
                            Subtotal = orderItem.Subtotal
                        };

                        if (orderItem.ProductVariant != null)
                        {
                            if (orderItem.ProductVariant.Product != null)
                            {
                                invoiceItem.ProductName = orderItem.ProductVariant.Product.Name;
                                
                                if (orderItem.ProductVariant.Product.Brand != null)
                                {
                                    invoiceItem.BrandName = orderItem.ProductVariant.Product.Brand.Name;
                                }
                            }

                            if (orderItem.ProductVariant.Size != null)
                            {
                                invoiceItem.SizeName = orderItem.ProductVariant.Size.Name;
                            }

                            if (orderItem.ProductVariant.Color != null)
                            {
                                invoiceItem.ColorName = orderItem.ProductVariant.Color.Name;
                            }
                        }

                        invoice.OrderItems.Add(invoiceItem);
                    }
                }
                var document = new InvoiceDocument(invoice);
                QuestPDF.Settings.License = LicenseType.Community;
                var pdfBytes = document.GeneratePdf();

                return File(pdfBytes, "application/pdf", $"HoaDon_{invoice.OrderCode}.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }
    }
}



