using KiraSoft.Application.IdentityViewModel;

namespace KiraSoft.Genealogy.WebAPI.Test.Areas.UserRegisterController.Data
{
    public static class Data_Mock
    {
        public static UserRegisterViewModel UserRegister_ViewModel_Ok() =>
            new UserRegisterViewModel()
            {
                Email = "test@test.com",
                FirstFamilyName = "Rincón",
                SecondFamilyName = "Olvera",
                Id = 0,
                IsFacebookAccount = false,
                IsGoogleAccount = false,
                Name = "Saúl",
                Password = "123$123Abcfs"
            };
    }
}
