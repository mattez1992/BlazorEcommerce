using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Server.Services.CartItemServices
{
    public class CartItemService : ICartItemService
    {
        private readonly DataContext _context;
        public CartItemService(DataContext context)
        {
            _context = context;

        }
        public Task<ServiceResponse<bool>> AddToCart(CartItem cartItem)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<int>> GetCartItemsCount()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<CartProductResponseDTO>>> GetCartProducts(List<CartItem> cartItems)
        {
            var result = new ServiceResponse<List<CartProductResponseDTO>>
            {
                Data = new List<CartProductResponseDTO>()
            };
            foreach (var item in cartItems)
            {
                var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == item.ProductId);

                if (product == null)
                {
                    continue;
                }

                var productVariant = await _context.ProductVariants
                    .Include(v => v.ProductType)
                    .FirstOrDefaultAsync(v => v.ProductId == item.ProductId && v.ProductTypeId == item.ProductTypeId);
                if(productVariant == null)
                {
                    continue ;
                }

                var cartProduct = new CartProductResponseDTO
                {
                    ProductId = product.Id,
                    Title = product.Title,
                    ImageUrl = product.ImageUrl,
                    Price = productVariant.Price,
                    ProductType = productVariant.ProductType.Name,
                    ProductTypeId = productVariant.ProductTypeId,
                    Quantity = item.Quantity
                };
                result.Data.Add(cartProduct);
            }

            return result;
        }

        public Task<ServiceResponse<List<CartProductResponseDTO>>> GetDbCartProducts(int? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<bool>> RemoveItemFromCart(int productId, int productTypeId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<CartProductResponseDTO>>> StoreCartItems(List<CartItem> cartItems)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<bool>> UpdateQuantity(CartItem cartItem)
        {
            throw new NotImplementedException();
        }
    }
}
