using Firebase.Database;
using Firebase.Auth;
using TMPro;
using UnityEngine;

public class FriendRequestSender : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInputField;

    public void SendFriendRequest()
    {
        string username = usernameInputField.text;

        // Buscar el ID único del usuario correspondiente al nombre de usuario ingresado
        DatabaseReference usersRef = FirebaseDatabase.DefaultInstance.GetReference("users");
        usersRef.OrderByChild("username").EqualTo(username).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error al buscar el usuario: " + task.Exception);
                return;
            }

            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.HasChildren)
                {
                    foreach (var childSnapshot in snapshot.Children)
                    {
                        string userIdDestino = childSnapshot.Key;
                        SendFriendRequest(userIdDestino);
                        return;
                    }
                }
                else
                {
                    Debug.LogWarning("No se encontró ningún usuario con el nombre proporcionado.");
                }
            }
        });
    }

    private void SendFriendRequest(string userIdDestino)
    {
        // Obtener el ID único del usuario actualmente autenticado
        string userIdOrigen = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        // Obtener una referencia a la ubicación de la solicitud de amistad en la base de datos
        DatabaseReference friendRequestRef = FirebaseDatabase.DefaultInstance
            .GetReference("friendRequests").Child(userIdDestino).Push();

        // Agregar la solicitud de amistad en la base de datos
        friendRequestRef.SetValueAsync(userIdOrigen);
    }
}
