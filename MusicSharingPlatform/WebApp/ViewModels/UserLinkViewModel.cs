using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class UserLinkViewModel
{
    public UserLink UserLink { get; set; } = default!;
    
    [ValidateNever]
    public SelectList UsersList { get; set; } = default!;
    
    [ValidateNever]
    public SelectList LinkTypesList { get; set; } = default!;
}