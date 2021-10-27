using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded => "Ürün eklendi";
        public static string ProductUpdated => "Ürün güncellendi";
        public static string ProductNameInvalid => "Ürün ismi geçersiz";
        public static string ProductCountOfCategoryError => "Bir kategoride en fazla 10 ürün olabilir";
        public static string ProductNameAlreadyExitsts => "Bu isimde zaten başka bir ürün var";

        public static string CategoryAdded => "Kategori eklendi";
        public static string CategoryDeleted => "Kategori eklendi";
        public static string CategoryUpdated => "Kategori güncellendi";

        public static string AuthorizationDenied => "Yetkiniz bulunmamaktadır!";
        public static string UserRegistered => "Kayıt oldu";
        public static string UserNotFound => "Kullanıcı bulunamadı";
        public static string PasswordError => "Parola hatası";
        public static string SuccessfulLogin => "Başarılı giriş";
        public static string UserAlreadyExists => "Kullanıcı mevcut";
        public static string AccessTokenCreated => "Token oluşturuldu";
    }
}
