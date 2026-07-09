using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateOfficeAPI.Models;
using RealEstateOfficeBusinessLogic;
using RealEstateOfficeDataAccess;
using RealStateOfficeModels.DTOs;
using RealStateOfficeModels.Users;

namespace RealStateOfficeAPI.Controllers
{
    [Authorize] //يعني لازم يكون معاك توكن لتوصل لاي داله موجوده جوا هذا الكنترولر
    [Route("api/Clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        [Authorize(Roles = "Admin")] //Admin only
        [HttpGet("All", Name = "GetAllClients")]
       
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Client>> GetAllClients()
        {
            //StudentDataSimulation.StudentsList.Clear();
            //StudentDataSimulation.Any() instead of count
            if (ClientsBL.GetAll().Any() == false)
            {
                return NotFound(new ErrorResponse
                {
                    Message = "No Students Found!",
                    Status = 404
                });
               
            }
            return Ok(ClientsBL.GetAll());
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("by-id/{id}", Name = "GetClientByClientID")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Client>> GetClientById(int id)
        {
            
            if (id < 1)
            {
                return BadRequest(new ErrorResponse
                {
                    Message = "No Accepted ID!",
                    Status = 400
                }
                    );
            }
         
            var client = ClientsBL.GetClientByClientID(id);
            if (client == null)
            {
                return NotFound(new ErrorResponse
                {
                    Message = $"Client with {id} Not Found!",
                    Status = 404
                }
                    );
            }

            return Ok(client);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost(Name = "AddClient")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ClientDTO> AddClient(ClientDTO client)
        {
            if (client == null || string.IsNullOrEmpty(client.Username) || string.IsNullOrEmpty(client.Email) || string.IsNullOrEmpty(client.Phone) || string.IsNullOrEmpty(client.Password))
            {
                return BadRequest(new ErrorResponse
                {
                    Message = "Invalid CLient Data. ",
                    Status = 400
                }
                   );
            }
           
            client.Password = BCrypt.Net.BCrypt.HashPassword(client.Password);

           if( ClientsBL.AddClient(client))
            {
                return Ok(value: "Done!");

            }
            else
            {
                return BadRequest(new ErrorResponse
                {
                    Message = "Error Adding Client. ",
                    Status = 400
                }
                   );
            }

        }


        [Authorize(Roles = "Admin")]
        [HttpPut("{ClientID}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]

        public ActionResult<string> UpdateClient(int ClientID, ClientDTO client)
        {
            if (ClientID <= 0||client == null || string.IsNullOrEmpty(client.Username) || string.IsNullOrEmpty(client.Email) || string.IsNullOrEmpty(client.Phone) || string.IsNullOrEmpty(client.Password))
            {
                return BadRequest(new ErrorResponse
                {
                    Message = "Invalid Client Data. ",
                    Status = 400
                }
                   );
            }

            var UserID = ClientsBL.GetUserIDByClientID(ClientID);
            if (ClientID == 0)
            {
                return NotFound(new ErrorResponse
                {
                    Message = $"Client with {ClientID} Not Found!",
                    Status = 404
                }
                   );
            }
                
            else
            {
                client.Password = BCrypt.Net.BCrypt.HashPassword(client.Password);

                ClientsBL.Update(UserID, client);

                return Ok($"Done .");

            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{ClientID}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]

        public ActionResult<string> DeleteClient(int ClientID)
        {
            if (ClientID <= 0)
            {
                return BadRequest(new ErrorResponse
                {
                    Message = "Invalid Client Data. ",
                    Status = 400
                }
                   );
            }



            else
            {
                int result = ClientsBL.Delete(ClientID);
                if (result==0)
                {
                    return NotFound(new ErrorResponse
                    {
                        Message = $"Client with {ClientID} Not Found!",
                        Status = 404
                    }
                       );
                }
                else
                    return Ok($"ID Client {ClientID} deleted");

            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<List<Client>> Search(string name)
        {
            var result = ClientsBL.Search(name);

            if (result == null)
            {
                return NotFound(new ErrorResponse
                {
                    Message = $"Not Found Clients. ",
                    Status = 404
                }
                   );
            }
            return Ok(result);
        }

        [Authorize(Roles = "Client")]
        [HttpGet("client-profile")]
        public IActionResult ClientProfile()
        {
           var claim = User.FindFirst("UserID");

            if(claim == null)
                return Unauthorized();

            int userID = int.Parse(claim.Value);

            var ClientID= ClientsBL.GetClientIDByUserID(userID);
            var user =
            ClientsBL.GetClientByClientID(
                ClientID
            );


            return Ok(user);
        }

        [Authorize(Roles = "Client")]
        [HttpPut("Client-Profile")]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]

        public ActionResult<string> UpdateProfile(ClientDTO client)
        {
            if (client == null || string.IsNullOrEmpty(client.Username) || string.IsNullOrEmpty(client.Email) || string.IsNullOrEmpty(client.Phone) || string.IsNullOrEmpty(client.Password))
            {
                return BadRequest(new ErrorResponse
                {
                    Message = "Invalid Client Data. ",
                    Status = 400
                }
                   );
            }
            var userIdClaim = User.FindFirst("UserID");

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var id = int.Parse(userIdClaim.Value);
            var ClientID = ClientsBL.GetClientIDByUserID(id);
            if (ClientID == 0)
            {
                return NotFound(new ErrorResponse
                {
                    Message = $"Client with {id} Not Found!",
                    Status = 404
                }
                   );
            }

            else
            {
                client.Password = BCrypt.Net.BCrypt.HashPassword(client.Password);

                ClientsBL.Update(id, client);

                return Ok($"Done .");

            }
        }

    }
}
