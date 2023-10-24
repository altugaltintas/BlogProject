using Blog_model.Models.Concrete;
using Blog_model.TypeConfigurations.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog_model.TypeConfigurations.Concrete
{
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.Property(a => a.Title).IsRequired(true);
            builder.Property(a => a.Content).IsRequired(true);

            // nap prop
            //category

            //builder.HasOne(a => a.Category).WithMany(a => a.Articles).HasForeignKey(a => a.CategoryID).OnDelete(DeleteBehavior.Restrict);




            //user

            builder.HasOne(a => a.AppUser).WithMany(a => a.Articles).HasForeignKey(a => a.AppUserID).OnDelete(DeleteBehavior.Restrict);
            /*
             Detele behavior :  silme davranışı  . mif efcore gelen bir enum yapasıdır. İlişkili enetiylerde slinme durumunda nasıl davranacağına karar vermesini sağlar

             Restrict      : ebeveyn çocuk ilişkisi yani evebeynsiz çokuk olaması gibi category slinmeye çalışıldığında makale kategorysiz kalacağı için müsade etemz

             No action     : İlişkilerde slinmeyi serbest bırakır hata vermez birşey yapmaz

             Cascede      : Evebeyn çocuk ilişkisinde evebeyn silindiğinde ona bağlı tüm çocukları siler

             Setnull     :  Defaulttaki slinme davranışıdır foreign key boş geçilememesini sağalr
             */
        }
    }
}
