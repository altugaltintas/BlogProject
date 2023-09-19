namespace Bloe_web.Areas.Member.Models.VMs
{
    public class GetArticleVM
    {

        //article

        public int ArticleID { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }

        //Categorty

        public string CategoryName { get; set; }



        //user

        public string FullName { get; set; }
    }
}
