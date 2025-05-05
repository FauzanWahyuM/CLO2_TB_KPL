using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tubes_Kelompok_BisaYukk.Modules;
using Xunit;

namespace Tubes_Kelompok_BisaYukk.Testing
{
    public class UserSessionTests
    {
        [Fact]
        public void Login_ShouldSetKaryawanAktif()
        {
            // Arrange
            var input = new StringReader("Sari");
            Console.SetIn(input);

            // Act
            UserSession.Login();
            var result = UserSession.GetKaryawanAktif();

            // Assert
            Assert.Equal("Sari", result);
        }
    }
}
