using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

namespace PruebasAutomatizadas
{
    [TestFixture]
    public class PruebasDeUsuario
    {
        private IWebDriver driver;

        [SetUp]
        public void Inicializar()
        {
            // Configuración del navegador
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.corotos.com.do/app_users/sign_in?redirect_to=https://www.corotos.com.do/");
        }

        [Test]
        public void IniciarSesionExitoso()
        {
            // Ingresa credenciales y hace clic en el botón de inicio de sesión
            driver.FindElement(By.Name("user[email]")).SendKeys("tu_usuario");
            driver.FindElement(By.Name("user[password]")).SendKeys("tu_contraseña");
            driver.FindElement(By.Name("commit")).Click();

           
            Assert.IsTrue(driver.Url.Contains("https://www.corotos.com.do/"));
        }

        [Test]
        public void ValidacionCamposObligatorios()
        {
            // Intenta enviar el formulario sin completar los campos requeridos
            driver.FindElement(By.Name("commit")).Click();

            // Verificar que se muestra un mensaje de error
            IWebElement errorMessage = driver.FindElement(By.CssSelector(".error-message"));
            Assert.IsNotNull(errorMessage);
        }

        [Test]
        public void RecuperarContrasena()
        {
            // Verifica que existe un enlace para recuperar la contraseña
            IWebElement recoveryLink = driver.FindElement(By.CssSelector(".recovery-link"));
            Assert.IsNotNull(recoveryLink);

            // Hace clic en el enlace de recuperación de contraseña
            recoveryLink.Click();

            // Verificar redirección a la página de restablecimiento de contraseña
            Assert.IsTrue(driver.Url.Contains("/reset_password"));
        }

        [TearDown]
        public void Finalizar()
        {
            // Cierra el navegador al finalizar las pruebas
            driver.Quit();
        }
    }
}
