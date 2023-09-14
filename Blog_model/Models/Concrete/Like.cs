namespace Blog_model.Models.Concrete
{
    public class Like
    {

        // NOT  like sınıfı baseEntitydern kalıtım alamz çünkü 1 beğeni bir kişi tarafından aynı makaleye 1 kere yapılabilir aynı nesne defalarca veritabanına eklenemez bu yüzden appUserID ver articleId anahtar olmalı ve eşsiz olmalı SQL tarafında ayrıca ıd verilerek eşsizliği bozulmamalıdır!!!!!



        //nav prop

        // 1 like 1 kullancıı tutar
        public string AppUserID { get; set; }   // 
        public AppUser AppUser { get; set; }

        // 1 like 1  makale beğenir

        public int ArticleID { get; set; }
        public Article Article { get; set; }
    }
}