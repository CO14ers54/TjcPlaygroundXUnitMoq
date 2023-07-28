using TjcPlaygroundXUnitMoqUat;

namespace TjcPlaygroundXUnitMoq
{
    public class ShoppingCartTest
    {
        public class DbServiceMock : IDbService
        {
            public bool ProcessResult { get; set; }

            public Product? ProductBeingProcessed { get; set; }

            public int? ProductIdBeingProcessed { get; set; }

            public bool RemoveItemFromShoppingCart(int? productId)
            {
                if (productId == null)
                    return false;

                ProductIdBeingProcessed = Convert.ToInt32(productId);
                return ProcessResult;
            }

            public bool SaveItemToShoppingCart(Product? product)
            {
                if (product == null) return false;

                ProductBeingProcessed = product;
                return ProcessResult;
            }
        }

        [Fact]
        public void AddProduct_Success()
        {
            // Given
            var dbMock = new DbServiceMock();
            dbMock.ProcessResult = true;

            var shoppingCart = new ShoppingCart(dbMock);

            // When
            var product = new Product(1, "shoes", 150.00);
            var result = shoppingCart.AddProduct(product);

            // Assert (Then)
            Assert.True(result);
            Assert.Equal(result, dbMock.ProcessResult);
            Assert.NotNull(dbMock.ProductBeingProcessed);
            Assert.Equal("shoes", dbMock.ProductBeingProcessed.name);
        }

        [Fact]
        public void AddProduct_Failure_DueToInvalidPayload()
        {
            // Given
            var dbMock = new DbServiceMock();
            dbMock.ProcessResult = false;

            var shoppingCart = new ShoppingCart(dbMock);

            // When
            var result = shoppingCart.AddProduct(null);

            // Assert
            Assert.False(result);
            Assert.Equal(result, dbMock.ProcessResult);
        }

        [Fact]
        public void RemoveProduct_Success()
        {
            // Given
            var dbMock = new DbServiceMock();
            dbMock.ProcessResult = true;

            var shoppingCart = new ShoppingCart(dbMock);

            // When
            var product = new Product(1, "shoes", 150.00);
            var result = shoppingCart.AddProduct(product);

            var deleteResult = shoppingCart.DeleteProduct(product.id);

            // Assert
            Assert.True(deleteResult);
            Assert.Equal(deleteResult, dbMock.ProcessResult);
        }
    }
}
