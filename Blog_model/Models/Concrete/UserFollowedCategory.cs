namespace Blog_model.Models.Concrete
{
    public class UserFollowedCategory
    {

        // ara tablodur buradak idler hem foreingkey hemde primarykey olacaktır yani composite key olacaktır

        // nav prop
        public string AppUserID { get; set; }
        public AppUser AppUser { get; set; }

        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}