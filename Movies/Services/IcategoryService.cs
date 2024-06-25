namespace Movies.Services
{
    public interface IcategoryService
    {
        Task <IEnumerable<Category>> GetAll ();
        Task <Category> Get (int id);
        Task <Category> Add (Category category);
        Task<Category> Update (Category category);
        Task<Category> Delete (Category category);
        Task<bool> IsValidCategory( int id);

    }
}
