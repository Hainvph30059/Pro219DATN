using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pro219.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DATN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    Delete = table.Column<bool>(type: "bit", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    Delete = table.Column<bool>(type: "bit", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Category_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HexCode = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    Delete = table.Column<bool>(type: "bit", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coupon",
                columns: table => new
                {
                    DiscountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DiscountType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinOrderValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupon", x => x.DiscountId);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    Delete = table.Column<bool>(type: "bit", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SaleValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Size", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    District = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OtherInfo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    Delete = table.Column<bool>(type: "bit", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cart_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    SaleId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    Delete = table.Column<bool>(type: "bit", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_Sale_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sale",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ShippingAddressId = table.Column<int>(type: "int", nullable: false),
                    DiscountId = table.Column<int>(type: "int", nullable: true),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: true),
                    OrderCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Order_Address_ShippingAddressId",
                        column: x => x.ShippingAddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Coupon_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Coupon",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Order_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: true),
                    SizeId = table.Column<int>(type: "int", nullable: true),
                    SKU = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ArrivalTime = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductVariant_Color_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ProductVariant_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductVariant_Size_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Size",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    UniqueID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Overall = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.UniqueID);
                    table.ForeignKey(
                        name: "FK_Review_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Review_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    VariantId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsSelectedForCheckout = table.Column<bool>(type: "bit", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItem_Cart_CartId",
                        column: x => x.CartId,
                        principalTable: "Cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItem_ProductVariant_VariantId",
                        column: x => x.VariantId,
                        principalTable: "ProductVariant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryLog",
                columns: table => new
                {
                    InventoryLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VariantId = table.Column<int>(type: "int", nullable: false),
                    ChangeQuantity = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryLog", x => x.InventoryLogId);
                    table.ForeignKey(
                        name: "FK_InventoryLog_ProductVariant_VariantId",
                        column: x => x.VariantId,
                        principalTable: "ProductVariant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductVariantId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_ProductVariant_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalTable: "ProductVariant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductVariantId = table.Column<int>(type: "int", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImage_ProductVariant_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalTable: "ProductVariant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductImage_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wishlist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ProductVariantId = table.Column<int>(type: "int", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wishlist_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wishlist_ProductVariant_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalTable: "ProductVariant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Brand",
                columns: new[] { "Id", "CreateAt", "Delete", "DeleteAt", "Description", "Name", "Status", "UpdateAt", "UpdateBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Thương hiệu thể thao hàng đầu thế giới", "Nike", (byte)1, null, "admin" },
                    { 2, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Thương hiệu thời trang thể thao nổi tiếng", "Adidas", (byte)1, null, "admin" },
                    { 3, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Thương hiệu thời trang thể thao đẳng cấp", "Puma", (byte)1, null, "admin" },
                    { 4, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Thương hiệu thời trang Nhật Bản", "Uniqlo", (byte)1, null, "admin" }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreateAt", "Delete", "DeleteAt", "Description", "Name", "ParentCategoryId", "Status", "UpdateAt", "UpdateBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Thời trang nam", "Nam", null, (byte)1, null, "admin" },
                    { 2, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Thời trang nữ", "Nữ", null, (byte)1, null, "admin" }
                });

            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "Id", "CreateAt", "Delete", "DeleteAt", "HexCode", "Name", "Status", "UpdateAt", "UpdateBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "#FF0000", "Đỏ", (byte)1, null, null },
                    { 2, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "#0000FF", "Xanh dương", (byte)1, null, null },
                    { 3, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "#000000", "Đen", (byte)1, null, null },
                    { 4, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "#FFFFFF", "Trắng", (byte)1, null, null },
                    { 5, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "#00FF00", "Xanh lá", (byte)1, null, null },
                    { 6, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "#808080", "Xám", (byte)1, null, null },
                    { 7, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "#FFC0CB", "Hồng", (byte)1, null, null }
                });

            migrationBuilder.InsertData(
                table: "Coupon",
                columns: new[] { "DiscountId", "Code", "CreateAt", "Delete", "DeleteAt", "DiscountType", "EndDate", "IsActive", "MinOrderValue", "StartDate", "Status", "UpdateAt", "UpdateBy", "Value" },
                values: new object[,]
                {
                    { 1, "MUAHHE20", new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Percentage", new DateTime(2024, 4, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), true, 500000m, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, null, "admin", 20m },
                    { 2, "CHAO10", new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Percentage", new DateTime(2024, 7, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), true, 300000m, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, null, "admin", 10m },
                    { 3, "BLACKFRIDAY30", new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Percentage", new DateTime(2024, 12, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, 1000000m, new DateTime(2024, 11, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, null, "admin", 30m }
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "CreateAt", "DateOfBirth", "Delete", "DeleteAt", "Email", "FullName", "LastLogin", "PasswordHash", "PhoneNumber", "Status", "UpdateAt", "UpdateBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "nguyenvanan@example.com", "Nguyễn Văn An", null, "b855e41c5c5f5061ecba4fd8613a7760", "0912345678", (byte)1, null, null },
                    { 2, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1992, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "tranthibinh@example.com", "Trần Thị Bình", null, "b855e41c5c5f5061ecba4fd8613a7760", "0987654321", (byte)1, null, null },
                    { 3, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1988, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "leminhcuong@example.com", "Lê Minh Cường", null, "b855e41c5c5f5061ecba4fd8613a7760", "0901234567", (byte)1, null, null }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethod",
                columns: new[] { "Id", "CreateAt", "Delete", "DeleteAt", "Description", "IsActive", "Name", "Status", "UpdateAt", "UpdateBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Thanh toán bằng tiền mặt khi nhận hàng", true, "Thanh toán khi nhận hàng", (byte)1, null, "admin" },
                    { 2, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Thanh toán bằng thẻ tín dụng", true, "Thẻ tín dụng", (byte)1, null, "admin" },
                    { 3, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Chuyển khoản qua ngân hàng", true, "Chuyển khoản ngân hàng", (byte)1, null, "admin" },
                    { 4, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Thanh toán qua ví điện tử (MoMo, ZaloPay)", true, "Ví điện tử", (byte)1, null, "admin" }
                });

            migrationBuilder.InsertData(
                table: "Sale",
                columns: new[] { "Id", "CreateAt", "Delete", "DeleteAt", "Description", "EndDate", "IsActive", "Name", "SaleValue", "StartDate", "Status", "Type", "UpdateAt", "UpdateBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Giảm giá mùa hè cho tất cả sản phẩm", new DateTime(2024, 4, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), true, "Khuyến mãi mùa hè", 20m, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, "Percentage", null, "admin" },
                    { 2, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Giảm giá mùa đông", new DateTime(2024, 10, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, "Khuyến mãi mùa đông", 15m, new DateTime(2024, 7, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, "Percentage", null, "admin" },
                    { 3, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Siêu sale Black Friday", new DateTime(2024, 12, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, "Khuyến mãi Black Friday", 30m, new DateTime(2024, 11, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, "Percentage", null, "admin" }
                });

            migrationBuilder.InsertData(
                table: "Size",
                columns: new[] { "Id", "CreateAt", "Delete", "DeleteAt", "Name", "Status", "UpdateAt", "UpdateBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "S", (byte)1, null, null },
                    { 2, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "M", (byte)1, null, null },
                    { 3, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "L", (byte)1, null, null },
                    { 4, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "XL", (byte)1, null, null },
                    { 5, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "XXL", (byte)1, null, null }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserID", "CreateAt", "PasswordHash", "Role", "Status", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "26dc318942685872cf79c5eb96c9bb13", "Admin", (byte)1, "admin" },
                    { 2, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "26dc318942685872cf79c5eb96c9bb13", "Manager", (byte)1, "manager" }
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "City", "CreateAt", "CustomerId", "Delete", "DeleteAt", "District", "FullName", "IsDefault", "OtherInfo", "Phone", "Status", "Street", "UpdateAt", "UpdateBy" },
                values: new object[,]
                {
                    { 1, "Hồ Chí Minh", new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, "Quận 1", "Nguyễn Văn An", true, "Chung cư ABC, căn hộ 4B", "0912345678", (byte)1, "123 Đường Nguyễn Huệ", null, null },
                    { 2, "Hà Nội", new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 2, false, null, "Quận Ba Đình", "Trần Thị Bình", true, null, "0987654321", (byte)1, "456 Đường Lê Lợi", null, null },
                    { 3, "Hồ Chí Minh", new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, "Quận 5", "Nguyễn Văn An", false, "Nhà riêng", "0912345678", (byte)1, "789 Đường Trần Hưng Đạo", null, null },
                    { 4, "Đà Nẵng", new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 3, false, null, "Quận Hải Châu", "Lê Minh Cường", true, null, "0901234567", (byte)1, "321 Đường Hoàng Diệu", null, null }
                });

            migrationBuilder.InsertData(
                table: "Cart",
                columns: new[] { "Id", "CreateAt", "CustomerId", "Delete", "DeleteAt", "SessionId", "Status", "UpdateAt", "UpdateBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null, (byte)1, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 2, false, null, null, (byte)1, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 3, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 3, false, null, null, (byte)1, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreateAt", "Delete", "DeleteAt", "Description", "Name", "ParentCategoryId", "Status", "UpdateAt", "UpdateBy" },
                values: new object[,]
                {
                    { 3, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Áo thun nam nữ", "Áo thun", 1, (byte)1, null, "admin" },
                    { 4, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Quần jean nam nữ", "Quần jean", 1, (byte)1, null, "admin" },
                    { 5, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Áo sơ mi công sở", "Áo sơ mi", 1, (byte)1, null, "admin" },
                    { 6, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Váy nữ", "Váy", 2, (byte)1, null, "admin" }
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "OrderId", "CreateAt", "CustomerId", "Delete", "DeleteAt", "DiscountAmount", "DiscountId", "FinalAmount", "LastUpdate", "Notes", "OrderCode", "OrderDate", "OrderStatus", "PaymentMethodId", "PaymentStatus", "ShippingAddressId", "Status", "TotalAmount", "UpdateBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, 279400m, 1, 1117600m, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), null, "DH001", new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "Đang xử lý", 1, "Chờ thanh toán", 1, (byte)1, 1397000m, "admin" },
                    { 2, new DateTime(2024, 1, 2, 10, 0, 0, 0, DateTimeKind.Unspecified), 2, false, null, 0m, null, 399000m, new DateTime(2024, 1, 2, 10, 0, 0, 0, DateTimeKind.Unspecified), null, "DH002", new DateTime(2024, 1, 2, 10, 0, 0, 0, DateTimeKind.Unspecified), "Đã giao hàng", 2, "Đã thanh toán", 2, (byte)1, 399000m, "admin" },
                    { 3, new DateTime(2024, 1, 3, 10, 0, 0, 0, DateTimeKind.Unspecified), 3, false, null, 49900m, 2, 449100m, new DateTime(2024, 1, 3, 10, 0, 0, 0, DateTimeKind.Unspecified), null, "DH003", new DateTime(2024, 1, 3, 10, 0, 0, 0, DateTimeKind.Unspecified), "Đang vận chuyển", 4, "Đã thanh toán", 4, (byte)1, 499000m, "admin" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "BasePrice", "BrandId", "CategoryId", "CreatedAt", "Delete", "DeleteAt", "Description", "Name", "SaleId", "Status", "UpdateAt", "UpdateBy" },
                values: new object[,]
                {
                    { 1, 299000m, 1, 3, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Áo thun nam chất liệu cotton mềm mại, thoáng mát, phù hợp mặc hàng ngày", "Áo thun nam Nike cổ tròn", 1, (byte)1, null, "admin" },
                    { 2, 799000m, 2, 4, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Quần jean nam kiểu dáng slim fit, chất liệu denim cao cấp, co giãn tốt", "Quần jean nam Adidas slim fit", null, (byte)1, null, "admin" },
                    { 3, 399000m, 3, 3, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Áo thun thể thao thấm hút mồ hôi tốt, phù hợp tập luyện và vận động", "Áo thun thể thao Puma", null, (byte)1, null, "admin" },
                    { 4, 499000m, 4, 5, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Áo sơ mi nam công sở, chất liệu cotton lụa, form dáng đẹp", "Áo sơ mi nam Uniqlo", null, (byte)1, null, "admin" },
                    { 5, 599000m, 2, 6, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Váy nữ thể thao, chất liệu thấm hút mồ hôi, thiết kế năng động", "Váy nữ Adidas", 1, (byte)1, null, "admin" }
                });

            migrationBuilder.InsertData(
                table: "ProductVariant",
                columns: new[] { "Id", "ArrivalTime", "ColorId", "CreateAt", "Delete", "DeleteAt", "IsActive", "Price", "ProductId", "SKU", "SizeId", "Status", "StockQuantity", "UpdateAt", "UpdateBy" },
                values: new object[,]
                {
                    { 1, null, 1, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, true, 299000m, 1, "NKE-TSH-RED-M", 2, (byte)1, 50, null, "admin" },
                    { 2, null, 2, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, true, 299000m, 1, "NKE-TSH-BLU-L", 3, (byte)1, 30, null, "admin" },
                    { 3, null, 3, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, true, 299000m, 1, "NKE-TSH-BLK-M", 2, (byte)1, 40, null, "admin" },
                    { 4, null, 3, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, true, 799000m, 2, "ADD-JNS-BLK-M", 2, (byte)1, 25, null, "admin" },
                    { 5, null, 6, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, true, 799000m, 2, "ADD-JNS-GRY-L", 3, (byte)1, 20, null, "admin" },
                    { 6, null, 4, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, true, 399000m, 3, "PMA-TSH-WHT-S", 1, (byte)1, 35, null, "admin" },
                    { 7, null, 4, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, true, 499000m, 4, "UNQ-SHT-WHT-M", 2, (byte)1, 15, null, "admin" },
                    { 8, null, 7, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, true, 599000m, 5, "ADD-DRS-PNK-M", 2, (byte)1, 18, null, "admin" }
                });

            migrationBuilder.InsertData(
                table: "Review",
                columns: new[] { "UniqueID", "Content", "CreatedAt", "CustomerId", "Delete", "DeleteAt", "Overall", "ProductId", "Status", "Title", "UpdateAt", "UpdateBy" },
                values: new object[,]
                {
                    { 1, "Áo rất mềm mại và thoáng mát, chất lượng đúng như mô tả. Tôi rất hài lòng với sản phẩm này!", new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, 5, 1, (byte)1, "Chất lượng tốt", null, null },
                    { 2, "Quần jean vừa vặn, chất liệu tốt, mặc rất đẹp. Sẽ mua thêm màu khác!", new DateTime(2024, 1, 3, 10, 0, 0, 0, DateTimeKind.Unspecified), 2, false, null, 5, 2, (byte)1, "Vừa vặn hoàn hảo", null, null },
                    { 3, "Áo thấm hút mồ hôi tốt, mặc tập gym rất thoải mái. Đáng giá tiền!", new DateTime(2024, 1, 4, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, 4, 3, (byte)1, "Phù hợp tập thể thao", null, null }
                });

            migrationBuilder.InsertData(
                table: "CartItem",
                columns: new[] { "Id", "AddedAt", "CartId", "CreateAt", "Delete", "DeleteAt", "IsSelectedForCheckout", "Quantity", "Status", "UnitPrice", "UpdateAt", "UpdateBy", "VariantId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, true, 2, (byte)1, 299000m, null, null, 1 },
                    { 2, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, false, 1, (byte)1, 799000m, null, null, 4 },
                    { 3, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, true, 1, (byte)1, 399000m, null, null, 6 },
                    { 4, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, true, 1, (byte)1, 499000m, null, null, 7 }
                });

            migrationBuilder.InsertData(
                table: "InventoryLog",
                columns: new[] { "InventoryLogId", "ChangeQuantity", "CreateAt", "Delete", "DeleteAt", "Reason", "Status", "UpdateAt", "UpdateBy", "VariantId" },
                values: new object[,]
                {
                    { 1, 50, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Nhập kho ban đầu", (byte)1, null, null, 1 },
                    { 2, 30, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Nhập kho ban đầu", (byte)1, null, null, 2 },
                    { 3, 40, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Nhập kho ban đầu", (byte)1, null, null, 3 },
                    { 4, 25, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Nhập kho ban đầu", (byte)1, null, null, 4 },
                    { 5, 20, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Nhập kho ban đầu", (byte)1, null, null, 5 },
                    { 6, 35, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Nhập kho ban đầu", (byte)1, null, null, 6 },
                    { 7, 15, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Nhập kho ban đầu", (byte)1, null, null, 7 },
                    { 8, 18, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Nhập kho ban đầu", (byte)1, null, null, 8 }
                });

            migrationBuilder.InsertData(
                table: "OrderItem",
                columns: new[] { "OrderItemId", "CreateAt", "Delete", "DeleteAt", "OrderId", "ProductVariantId", "Quantity", "Status", "Subtotal", "UnitPrice", "UpdateAt", "UpdateBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, 1, 1, 2, (byte)1, 598000m, 299000m, null, null },
                    { 2, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, 1, 4, 1, (byte)1, 799000m, 799000m, null, null },
                    { 3, new DateTime(2024, 1, 2, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, 2, 6, 1, (byte)1, 399000m, 399000m, null, null },
                    { 4, new DateTime(2024, 1, 3, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, 3, 7, 1, (byte)1, 499000m, 499000m, null, null }
                });

            migrationBuilder.InsertData(
                table: "ProductImage",
                columns: new[] { "Id", "CreateAt", "Delete", "DeleteAt", "ImageUrl", "IsMain", "ProductId", "ProductVariantId", "Status", "UpdateAt", "UpdateBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "/images/products/ao-thun-nike-do-m-1.jpg", true, 1, 1, (byte)1, null, "admin" },
                    { 2, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "/images/products/ao-thun-nike-do-m-2.jpg", false, 1, 1, (byte)1, null, "admin" },
                    { 3, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "/images/products/quan-jean-adidas-den-m-1.jpg", true, 2, 4, (byte)1, null, "admin" },
                    { 4, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "/images/products/ao-thun-puma-trang-s-1.jpg", true, 3, 6, (byte)1, null, "admin" },
                    { 5, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "/images/products/ao-so-mi-uniqlo-trang-m-1.jpg", true, 4, 7, (byte)1, null, "admin" },
                    { 6, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), false, null, "/images/products/vay-nu-adidas-hong-m-1.jpg", true, 5, 8, (byte)1, null, "admin" }
                });

            migrationBuilder.InsertData(
                table: "Wishlist",
                columns: new[] { "Id", "CreateAt", "CustomerId", "Delete", "DeleteAt", "ProductVariantId", "Status", "UpdateAt", "UpdateBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, 2, (byte)1, null, null },
                    { 2, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, 6, (byte)1, null, null },
                    { 3, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 2, false, null, 1, (byte)1, null, null },
                    { 4, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 3, false, null, 5, (byte)1, null, null },
                    { 5, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 3, false, null, 8, (byte)1, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_CustomerId",
                table: "Address",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_CustomerId",
                table: "Cart",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_CartId",
                table: "CartItem",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_VariantId",
                table: "CartItem",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentCategoryId",
                table: "Category",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Coupon_Code",
                table: "Coupon",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_Email",
                table: "Customer",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLog_VariantId",
                table: "InventoryLog",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_DiscountId",
                table: "Order",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderCode",
                table: "Order",
                column: "OrderCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_PaymentMethodId",
                table: "Order",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ShippingAddressId",
                table: "Order",
                column: "ShippingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductVariantId",
                table: "OrderItem",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_BrandId",
                table: "Product",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SaleId",
                table: "Product",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductId",
                table: "ProductImage",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductVariantId",
                table: "ProductImage",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariant_ColorId",
                table: "ProductVariant",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariant_ProductId",
                table: "ProductVariant",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariant_SizeId",
                table: "ProductVariant",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariant_SKU",
                table: "ProductVariant",
                column: "SKU",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Review_CustomerId",
                table: "Review",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_ProductId",
                table: "Review",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_CustomerId",
                table: "Wishlist",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_ProductVariantId",
                table: "Wishlist",
                column: "ProductVariantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "InventoryLog");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "ProductImage");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Wishlist");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "ProductVariant");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Coupon");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Size");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Sale");
        }
    }
}
