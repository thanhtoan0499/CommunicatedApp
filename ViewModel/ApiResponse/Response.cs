using DatingApp.Models;
using System.Collections.Generic;

namespace DatingApp.ViewModel.ApiResponse
{
    public class Response
    {

        public Response()
        {
        }

        public Response(object v)
        {
            Message = v;
            StatusCode = 200;
        }

        public Response(object v, StatusType code)
        {
            Message = v;
            switch (code)
            {
                case StatusType.SUCCESS:
                    StatusCode = 200;
                    break;
                case StatusType.UNAUTHORIZED:
                    StatusCode = 401;
                    break;
                case StatusType.FORBIDDEN:
                    StatusCode = 403;
                    break;
                case StatusType.NOT_FOUND:
                    StatusCode = 404;
                    break;
                case StatusType.BAD_REQUEST:
                    StatusCode = 400;
                    break;
                case StatusType.SERVER_ERROR:
                    StatusCode = 500;
                    break;
                default:
                    StatusCode = 200;
                    break;
            }
        }

        public int? StatusCode { get; set; }

        public object? Message { get; set; }

       
    }
}
