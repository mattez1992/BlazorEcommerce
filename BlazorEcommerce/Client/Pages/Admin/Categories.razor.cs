namespace BlazorEcommerce.Client.Pages.Admin
{
    public partial class Categories : ComponentBase, IDisposable
    {
        [Inject]
        public ICategoryService CategoryService { get; set; }
        Category editingCategory = null;

        protected override async Task OnInitializedAsync()
        {
            await CategoryService.GetAdminCategories();
            CategoryService.OnChange += StateHasChanged;
        }

        public void Dispose()
        {
            CategoryService.OnChange -= StateHasChanged;
        }

        private void CreateNewCategory()
        {
            editingCategory = CategoryService.CreateNewCategory();
        }

        private void EditCategory(Category category)
        {
            category.Editing = true;
            editingCategory = category;
        }

        private async Task UpdateCategory()
        {
            if (editingCategory.IsNew)
                await CategoryService.AddCategory(editingCategory);
            else
                await CategoryService.UpdateCategory(editingCategory);
            editingCategory = null;
        }

        private async Task CancelEditing()
        {
            editingCategory = null;
            await CategoryService.GetAdminCategories();
        }

        private async Task DeleteCategory(int id)
        {
            await CategoryService.DeleteCategory(id);
        }
    }
}
