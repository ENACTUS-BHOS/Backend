namespace Backend.Infrastructure.Repositories;

using System.Net;
using System.Net.Mail;
using System.Text;
using Backend.Core.Models;
using Backend.Core.Repositories;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class ProductsSqlRepository : IProductsRepository
{
    private readonly MirasDbContext dbContext;

    public ProductsSqlRepository(MirasDbContext dbContext) => this.dbContext = dbContext;

    public IEnumerable<Product> GetAll()
    {
        var products = this.dbContext.Products.Where(product => product.IsActive == true);

        return products;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        var product = await this.dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

        return product;
    }

    public IEnumerable<Product> TakeTop(int count)
    {
        var products = this.dbContext.Products.Take(count);

        return products.AsEnumerable();
    }

    public async Task AddAsync(Product product)
    {
        await this.dbContext.Products.AddAsync(product);

        await this.dbContext.SaveChangesAsync();
    }

    public async Task OrderAsync(Order order)
    {
        var product = await GetByIdAsync((int)order.ProductId!);

        var artist = await this.dbContext.Artists.FirstOrDefaultAsync(a => a.Id == product!.ArtistId);

        using (MailMessage mail = new MailMessage())
        {
            mail.From = new MailAddress("emil.abdullayev.std@bhos.edu.az");
            
            mail.To.Add("emil.abdullayev.std@bhos.edu.az");
            
            mail.Subject = "Order";

            var width = "435";

            var height = "500";

            mail.Body = $"<h3>Name: {order.FullName}</h3><h4>Email address: {order.Email}</h4><h4>Phone number: {order.PhoneNumber}</h4><div><b>Product: </b>{product!.Name} by {artist!.Name}</div><br><div><b>Additional notes: </b>{order.AdditionalInformation}</div><br><div><b>Product image link: <a href={product.ImageUrl}>{product.ImageUrl}</a></b></div>";
            
            mail.IsBodyHtml = true;

            mail.BodyEncoding = Encoding.UTF8;

            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            using (SmtpClient client = new SmtpClient("smtp-mail.outlook.com", 587))
            {
                client.EnableSsl = true;

                client.UseDefaultCredentials = false;

                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                
                client.Credentials = new NetworkCredential("emil.abdullayev.std@bhos.edu.az", "2s2MrL51");
                
                client.Send(mail);
            }
        }

        await this.dbContext.Orders.AddAsync(order);

        await this.dbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(int id)
    {
        var product = await this.dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

        product!.IsActive = false;

        await this.dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, Product newProduct)
    {
        var product = await this.dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

        product!.Name = newProduct.Name;
        
        product!.ArtistId = newProduct.ArtistId;
        
        product!.ImageUrl = newProduct.ImageUrl;
        
        product!.Price = newProduct.Price;
        
        product!.IsActive = newProduct.IsActive;

        await this.dbContext.SaveChangesAsync();
    }

    public IEnumerable<Product> GetByAuthorId(int authorId)
    {
        var products = this.dbContext.Products.Where(product => product.ArtistId == authorId);

        return products;
    }

    public IEnumerable<Product> SortByPrice(int authorId, bool isAscending)
    {
        var products = this.dbContext.Products.Where(product => product.ArtistId == authorId);

        if (isAscending)
        {
            return products.OrderBy(product => product.Price).AsEnumerable();
        }

        return products.OrderByDescending(product => product.Price).AsEnumerable();
    }

    public IEnumerable<Product> FilterByPrice(int authorId, int minimumPrice, int maximumPrice)
    {
        var products = this.dbContext.Products.Where(product => product.ArtistId == authorId);
        
        products = products.Where(product => product.Price >= minimumPrice && product.Price <= maximumPrice);

        return products;
    }
}