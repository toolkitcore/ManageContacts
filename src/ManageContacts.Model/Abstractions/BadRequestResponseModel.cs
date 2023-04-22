using System.Net;

namespace ManageContacts.Model.Abstractions;

public class BadRequestResponseModel : BaseResponseModel
{
    public BadRequestResponseModel() { }

    public BadRequestResponseModel(string errorMessage) : base(HttpStatusCode.BadRequest, errorMessage) { }
}