using AutoMapper;
using Common;
using Microsoft.AspNetCore.Mvc;
using MKDIR.Domain;
using MKDIR.WebApi.Controllers;
using System.Net;

namespace MKDIR.WebApi
{
    public class BusinessUserController : ApiControllerBase
    {
        private readonly IBusinessUserService _service;
        private readonly IMapper _mapper;

        public BusinessUserController(IBusinessUserService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _service.GetAsync(id);

                if(result is null)
                    return Response(null, 0, HttpStatusCode.BadRequest, true, "La consulta no devolvió ningún resultado");
                else
                    return Response(result, 1, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Response(null, 0, HttpStatusCode.BadRequest, true, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BusinessUserCreateDTO dto)
        {
            try
            {
                BusinessUser result = null;
                if (dto == null)
                {
                    return Response(statusCode: HttpStatusCode.BadRequest, hasErrors: true, errors: Constants.INVALID_DATA);
                }

                var _entity = _mapper.Map<BusinessUser>(dto);
                //_entity.feCreacion = System.DateTime.Now;
                result = await _service.PostAsync(_entity);

                var _dtoresult = _mapper.Map<BusinessUserDTO>(result);
                return Response(_dtoresult);

            }
            catch (Exception ex)
            {
                return Response(statusCode: HttpStatusCode.BadRequest, hasErrors: true, errors: ex);
            }
        }
    }
}
