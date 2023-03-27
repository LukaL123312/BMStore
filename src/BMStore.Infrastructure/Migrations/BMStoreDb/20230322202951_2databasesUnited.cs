using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMStore.Infrastructure.Migrations.BMStoreDb
{
    /// <inheritdoc />
    public partial class _2databasesUnited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryEntity_CategoryEntity_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "CategoryEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LayoutEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainColor = table.Column<int>(type: "int", nullable: false),
                    BackgroundColor = table.Column<int>(type: "int", nullable: false),
                    BackgroundImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SliderImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeaderStyle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FooterStyle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherStyling = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Font = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LayoutEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionForAdmin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsInStock = table.Column<bool>(type: "bit", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Volume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceOnSale = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsOnSale = table.Column<bool>(type: "bit", nullable: false),
                    SaleDuration = table.Column<TimeSpan>(type: "time", nullable: false),
                    UserRating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductEntity_CategoryEntity_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CategoryEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductEntity_ProductEntity_ProductEntityId",
                        column: x => x.ProductEntityId,
                        principalTable: "ProductEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ImageEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageEntity_ProductEntity_ProductEntityId",
                        column: x => x.ProductEntityId,
                        principalTable: "ProductEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KeywordEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Keyword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeywordEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeywordEntity_ProductEntity_ProductEntityId",
                        column: x => x.ProductEntityId,
                        principalTable: "ProductEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AddressEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressOwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommentEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentedProductId = table.Column<int>(type: "int", nullable: true),
                    CommentAuthorId = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentEntity_ProductEntity_CommentedProductId",
                        column: x => x.CommentedProductId,
                        principalTable: "ProductEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PackageEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackagePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaymentOption = table.Column<int>(type: "int", nullable: false),
                    LayoutId = table.Column<int>(type: "int", nullable: false),
                    UserEntityId = table.Column<int>(type: "int", nullable: true),
                    UserEntityId1 = table.Column<int>(type: "int", nullable: true),
                    UserEntityId2 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackageEntity_LayoutEntity_LayoutId",
                        column: x => x.LayoutId,
                        principalTable: "LayoutEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubDomainName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackageEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserEntities_PackageEntity_PackageEntityId",
                        column: x => x.PackageEntityId,
                        principalTable: "PackageEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressEntity_AddressOwnerId",
                table: "AddressEntity",
                column: "AddressOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryEntity_ParentCategoryId",
                table: "CategoryEntity",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentEntity_CommentAuthorId",
                table: "CommentEntity",
                column: "CommentAuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentEntity_CommentedProductId",
                table: "CommentEntity",
                column: "CommentedProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageEntity_ProductEntityId",
                table: "ImageEntity",
                column: "ProductEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordEntity_ProductEntityId",
                table: "KeywordEntity",
                column: "ProductEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageEntity_LayoutId",
                table: "PackageEntity",
                column: "LayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageEntity_UserEntityId",
                table: "PackageEntity",
                column: "UserEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageEntity_UserEntityId1",
                table: "PackageEntity",
                column: "UserEntityId1");

            migrationBuilder.CreateIndex(
                name: "IX_PackageEntity_UserEntityId2",
                table: "PackageEntity",
                column: "UserEntityId2");

            migrationBuilder.CreateIndex(
                name: "IX_ProductEntity_CategoryId",
                table: "ProductEntity",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductEntity_ProductEntityId",
                table: "ProductEntity",
                column: "ProductEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEntities_PackageEntityId",
                table: "UserEntities",
                column: "PackageEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AddressEntity_UserEntities_AddressOwnerId",
                table: "AddressEntity",
                column: "AddressOwnerId",
                principalTable: "UserEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentEntity_UserEntities_CommentAuthorId",
                table: "CommentEntity",
                column: "CommentAuthorId",
                principalTable: "UserEntities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PackageEntity_UserEntities_UserEntityId",
                table: "PackageEntity",
                column: "UserEntityId",
                principalTable: "UserEntities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PackageEntity_UserEntities_UserEntityId1",
                table: "PackageEntity",
                column: "UserEntityId1",
                principalTable: "UserEntities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PackageEntity_UserEntities_UserEntityId2",
                table: "PackageEntity",
                column: "UserEntityId2",
                principalTable: "UserEntities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PackageEntity_UserEntities_UserEntityId",
                table: "PackageEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_PackageEntity_UserEntities_UserEntityId1",
                table: "PackageEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_PackageEntity_UserEntities_UserEntityId2",
                table: "PackageEntity");

            migrationBuilder.DropTable(
                name: "AddressEntity");

            migrationBuilder.DropTable(
                name: "CommentEntity");

            migrationBuilder.DropTable(
                name: "ImageEntity");

            migrationBuilder.DropTable(
                name: "KeywordEntity");

            migrationBuilder.DropTable(
                name: "ProductEntity");

            migrationBuilder.DropTable(
                name: "CategoryEntity");

            migrationBuilder.DropTable(
                name: "UserEntities");

            migrationBuilder.DropTable(
                name: "PackageEntity");

            migrationBuilder.DropTable(
                name: "LayoutEntity");
        }
    }
}
