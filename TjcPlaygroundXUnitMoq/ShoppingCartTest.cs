using Moq;
using TjcPlaygroundXUnitMoqUat;

namespace TjcPlaygroundXUnitMoq
{
    public class ShoppingCartTest
    {
        public readonly Mock<IDbService> m_dbServiceMock = new();

        [Fact]
        public void AddProduct_Success()
        {
            // Given
            var shoppingCart = new ShoppingCart(m_dbServiceMock.Object);

            // When
            var product = new Product(1, "shoes", 150.00);
            var result = shoppingCart.AddProduct(product);

            // Assert (Then)
            Assert.True(result);
            m_dbServiceMock.Verify(x => x.SaveItemToShoppingCart(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public void AddProduct_Failure_DueToInvalidPayload()
        {
            // Given
            var shoppingCart = new ShoppingCart(m_dbServiceMock.Object);

            // When
            var result = shoppingCart.AddProduct(null);

            // Assert
            Assert.False(result);
            m_dbServiceMock.Verify(x => x.SaveItemToShoppingCart(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public void RemoveProduct_Success()
        {
            // Given
            var shoppingCart = new ShoppingCart(m_dbServiceMock.Object);

            // When
            var product = new Product(1, "shoes", 150.00);
            var result = shoppingCart.AddProduct(product);

            var deleteResult = shoppingCart.DeleteProduct(product.id);

            // Assert
            Assert.True(deleteResult);
            m_dbServiceMock.Verify(x => x.SaveItemToShoppingCart(It.IsAny<Product>()), Times.Once);
        }
    }
}
