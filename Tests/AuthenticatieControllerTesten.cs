namespace backend_tests
{
	using Moq;
	using Accessibility_backend.Services;
	using Accessibility_backend;
	using Microsoft.AspNetCore.Mvc;
	using Accessibility_backend.Modellen.Registreermodellen;
	using Accessibility_backend.Modellen.DTO;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc.Routing;
    using Accessibility_app.Data;
    using Accessibility_app.Models;
    using Accessibility_backend.Modellen.Extra;
    using Microsoft.AspNetCore.Identity;
	using Microsoft.Extensions.Configuration;
    using NuGet.Common;
    using System.IdentityModel.Tokens.Jwt;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

	public class AuthenticatieControllerTesten
	{
		private Mock<IEmailSender> mockEmailer;
		private Mock<IAuthenticatieService> mockAuthService;
        private Mock<Rol> mockrol;
        private AuthenticatieController controller;

		public AuthenticatieControllerTesten()
		{
			mockEmailer = new Mock<IEmailSender>();
			mockAuthService = new Mock<IAuthenticatieService>();
			mockrol = new Mock<Rol>();

        }
		[Fact]
		public async void EmailVerzendenReturnsOk()
		{
			//arrange
			mockEmailer.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
				.ReturnsAsync(new Response { Status = "Success", Message = "Mocked success message" });

			mockAuthService.Setup(x => x.RegistreerErvaringsdeskundige(It.IsAny<RegistrerenErvaringdeskundige>()))
				.ReturnsAsync(new OkObjectResult(new VoorbereidingEmailModel{ Email = "test@example.com", Token = "mockedToken" }));

			var mockUrlHelper = new Mock<IUrlHelper>();
			mockUrlHelper.Setup(x => x.Action(It.IsAny<UrlActionContext>())).Returns("/mocked-action");

			controller = new AuthenticatieController(mockAuthService.Object, mockEmailer.Object);
			controller.Url = mockUrlHelper.Object;
			var httpContext = new DefaultHttpContext { Request = { Scheme = "http" }};
			controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

			//act
			var result = await controller.RegistreerErvaringsdeskundige(new RegistrerenErvaringdeskundige());
			Console.WriteLine(result);

			//assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var resultValue = Assert.IsType<Response>(okResult.Value);

			Assert.Equal("Success", resultValue.Status);
			Assert.Equal("Mocked success message", resultValue.Message);
		}

		[Fact]
		public async Task VerwijderGebruikerReturnsNotFound()
		{
			//het verwacht een id van type string
			//arrange
			mockAuthService.Setup(x => x.VerwijderGebruiker(It.IsAny<string>()))
				.ReturnsAsync(new NotFoundObjectResult(new Response() { Status = "Error", Message = "Gebruiker niet gevonden" }));
			controller = new AuthenticatieController(mockAuthService.Object, mockEmailer.Object);

			//act
			var result = await controller.VerwijderGebruiker("14");

			var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
			var resultValue = Assert.IsType<Response>(notFoundResult.Value);

			//assert
			Assert.Equal("Error", resultValue.Status);
			Assert.Equal(404, notFoundResult.StatusCode);
		}


		[Fact]
		public async Task LoginReturnsOk()
		{
			mockAuthService.Setup(x => x.Login(It.IsAny<LoginModel>())).ReturnsAsync(new OkObjectResult(new
			{
				token = "mockToken",
				expiration = "mockValue",
				roles = "userrole"
			}));

            controller = new AuthenticatieController(mockAuthService.Object, mockEmailer.Object);
			var model = new LoginModel { Email = "test@example.com", Wachtwoord = "Qwe123@" };

			//act
			var result = await controller.Login(model);
            var okResult = Assert.IsType<OkObjectResult>(result);

            //assert
            var resultValue = okResult.Value as dynamic;
            Assert.Equal("mockToken", resultValue.token);
            Assert.Equal("mockValue", resultValue.expiration);
            Assert.Equal("userrole", resultValue.roles);
   
        }
    }
}