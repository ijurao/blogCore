using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Blog.AccesoDatos.Data;
using Blog.AccesoDatos.Data.Repository;
using Blog.Modelos;
using Blog.Modelos.Utilidades;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace Blog.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IContenedorTrabajo _container;
        [PageRemote(
       ErrorMessage = "Email Address already exists",
       AdditionalFields = "__RequestVerificationToken",
       HttpMethod = "post",
       PageHandler = "CheckEmail"
   ),Display(Name = "Nombre del usuario")]
        [BindProperty]

        public string Nombre {get; set; }
        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
           IContenedorTrabajo container
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _container = container;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]   
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar password")]
            [Compare("Password", ErrorMessage = "Password no coinciden.")]
            public string ConfirmPassword { get; set; }

            public string Nombre { get; set; }

            public string Direccion { get; set; }

            public string Ciudad { get; set; }
            public string Pais { get; set; }
            public string PhoneNumber { get; set; }

        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email,
                    Email = Input.Email,
                    Nombre = this.Nombre,
                    Ciudad = Input.Ciudad,
                    Pais = Input.Pais,
                    Direccion = Input.Direccion,
                    PhoneNumber = Input.PhoneNumber,
                    EmailConfirmed = true};
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                     // validamos si los roles existen si no se crean
                    if(! await _roleManager.RoleExistsAsync(Constantes.admin))
                    {
                       await _roleManager.CreateAsync(new IdentityRole(Constantes.admin));
                       await _roleManager.CreateAsync(new IdentityRole(Constantes.cliente)) ;


                    }

                    // obtener el rol seleccionado

                    var rol = Request.Form["Rol"].ToString();

                    // validamos si el rol seleccionado es admin y si lo es lo agregamos
                    
                    if(rol == Constantes.admin)
                    {
                        await _userManager.AddToRoleAsync(user,rol);
                    }
                    else
                    {
                        if(rol == Constantes.cliente)
                        {
                            await _userManager.AddToRoleAsync(user, rol);

                        }
                        else
                        {

                            await _userManager.AddToRoleAsync(user, Constantes.cliente);

                        }
                    }
                    _logger.LogInformation("User created a new account with password.");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);

                 
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();

            // If we got this far, something failed, redisplay form
        }

        public async Task< JsonResult> OnPostCheckEmail()
        {
            var userParam = this.Nombre;

            var user = await _userManager.FindByNameAsync(Nombre);
            if (user != null) return new JsonResult(false); 
            return new  JsonResult(true);
        }

    }
}
