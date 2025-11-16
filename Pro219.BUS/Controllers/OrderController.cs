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
                return StatusCode(500, $"An error occurred: {ex.Message}");
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
                    return NotFound("Order not found");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
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
                    return NotFound("Order not found");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
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
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<Order>> AddOrder([FromBody] Order order)
        {
            try
            {
                if (order == null)
                {
                    return BadRequest("Order data is required");
                }

                var result = await orderRepository.AddOrder(order);
                if (result == null)
                {
                    return StatusCode(500, "Failed to add order");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult<Order>> UpdateOrder([FromBody] Order order)
        {
            try
            {
                if (order == null)
                {
                    return BadRequest("Order data is required");
                }

                order.UpdateBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var result = await orderRepository.UpdateOrder(order);
                if (result == null)
                {
                    return NotFound("Order not found or update failed");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
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
                    return NotFound("Order not found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
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
                    return NotFound("Order not found");
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
                return StatusCode(500, $"An error occurred: {ex.Message}");
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
                    return NotFound("Order not found");
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
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}

