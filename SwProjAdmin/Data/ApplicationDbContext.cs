using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SwProjAdmin.Models;
using Attribute = SwProjAdmin.Models.Attribute;

namespace SwProjAdmin.Data;

public class ApplicationDbContext : IdentityDbContext
{
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Price> Prices { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<GalleryItem> GalleryItems { get; set; }
    public DbSet<AttributeSet> AttributeSets { get; set; }
    public DbSet<Attribute> Attributes { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Category to List<Product>
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)        // Each Product has one Category
            .WithMany(c => c.Products)     // Each Category can have many Products
            .HasForeignKey(p => p.CategoryId);              // The foreign key in Product is CategoryId
        
        //Product to List<GalleryItem>
        modelBuilder.Entity<Product>()
            .HasMany<GalleryItem>(p => p.GalleryItems)     // Each Product has many GalleryItems
            .WithOne(g => g.Product)                    // Each GalleryItems can have one Product
            .HasForeignKey(p => p.ProductId);                           // The foreign key in GalleryItem is ProductId
        
        //Product to List<Price>
        modelBuilder.Entity<Product>()
            .HasMany<Price>(product => product.Prices)     // Each Product has many Prices
            .WithOne(price => price.Product)               // Each Price can have one Product
            .HasForeignKey(p => p.ProductId);                         // The foreign key in Price is ProductId
        
        //Product to List<AttributeSet>
        modelBuilder.Entity<Product>()
            .HasMany<AttributeSet>(product => product.AttributeSets)     // Each Product has many AttributeSets
            .WithOne(attributeSet => attributeSet.Product)               // Each AttributeSet can have one Product
            .HasForeignKey(p => p.ProductId);                                  // The foreign key in Price is ProductId
        
        //Currency to List<Price>
        modelBuilder.Entity<Currency>()
            .HasMany<Price>(p => p.Prices)     // Price has one Currency
            .WithOne(c => c.Currency)             // Currency has one Price
            .HasForeignKey(c => c.CurrencyId);                    // The foreign key in Currency is PriceId
        
        //AttributeSet to List<Attribute>
        modelBuilder.Entity<AttributeSet>()
            .HasMany<Attribute>(p => p.Attributes)            // AttributeSet has many Attributes
            .WithOne(a => a.AttributeSet)                       // Attribute has one AttributeSet
            .HasForeignKey(c => c.AttributeSetId);                              // The foreign key in Currency is PriceId
        
        base.OnModelCreating(modelBuilder);
    }
}