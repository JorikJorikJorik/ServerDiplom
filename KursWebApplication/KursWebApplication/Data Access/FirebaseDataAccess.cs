using System.Linq;
using System.Net;
using KursWebApplication.Models;
using Newtonsoft.Json;

namespace KursWebApplication.Data_Access
{
    public class FirebaseDataAccess
    {
        public void addToken(int userId, string token)
        {
            saveToken(userId, token);
        }

        public void refreshToken(int userId, string newToken)
        {
            saveToken(userId, newToken);
        }

        private void saveToken(int userId, string token)
        {
            var db = new MyDBModels.DB();
            var userModel = db.user.Where(u => u.UserId == userId).First();
            userModel.FirebaseToken = token;
            db.SaveChanges();
        }


        public void sendRequestToFirebase(string data, int typePush, string token)
        {
            var firebaseSendMessageModel = new FirebaseSendMessageModel();
            var firebaseDataModel = new FirebaseDataModel();

            firebaseDataModel.dataModel = data;
            firebaseDataModel.typePush = typePush;

            firebaseSendMessageModel.data = firebaseDataModel;
            firebaseSendMessageModel.to = token;

            var json = JsonConvert.SerializeObject(firebaseSendMessageModel);

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Headers[HttpRequestHeader.Authorization] = "key=AIzaSyAQD2wfyz55UVwHjhWEPoEUmKk1-T14Swo";//поменяй его!!!!
                client.UploadString("https://fcm.googleapis.com/fcm/send", "POST", json);
            }
       
        }
    }
}