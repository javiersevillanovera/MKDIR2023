using AutoMapper;
using Common;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MKDIR.Domain;
using MKDIR.Infrastructure;
using MKDIR.WebApi.Controllers;
using System.Net;

namespace MKDIR.WebApi
{
    [Authorize]
    public class BusinessModuleController : ApiControllerBase
    {
        private readonly IBusinessModuleService _service;
        private readonly IMapper _mapper;

        public BusinessModuleController(IBusinessModuleService service, IMapper maper)
        {
            _service = service;
            _mapper = maper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _service.GetAsync(id);

                if (result is null)
                    return Response(null, 0, HttpStatusCode.BadRequest, true, "La consulta no devolvió ningún resultado");
                else
                    return Response(_mapper.Map<BusinessModuleDTO>(result), 1);
            }
            catch (Exception ex)
            {
                return Response(null, 0, HttpStatusCode.BadRequest, true, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Domain.BaseFilter filter)
        {
            try
            {
                var query = _service.Get();

                if (filter != null)
                {
                    if (filter.Id.HasValue)
                    {
                        query = query.Where(x => x.BusinessModuleId == filter.Id);
                    }

                    if (!string.IsNullOrEmpty(filter.Search))
                    {
                        var q = filter.Search.Trim();
                        query = query.Where(x =>
                                                x.Name.Contains(q)
                                            );
                    }

                    if (filter.SearchFilter != null)
                    {
                        query = query.ApplyFilter(filter.SearchFilter);
                    }
                }

                var count = query.Count();

                query = query.OrderBy(x => x.BusinessModuleId);
                query = query.Paginate(filter.page, filter.pageSize);

                var entityTypes = query.ToList();

                //var result = _mapper.Map<BusinessModuleDTO>(entityTypes);

                var result = entityTypes.Select(x => _mapper.Map<BusinessModuleDTO>(x)).ToList();

                return Response(result, count, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Response(null, 0, HttpStatusCode.BadRequest, true, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BusinessModuleDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    return Response(statusCode: HttpStatusCode.BadRequest, hasErrors: true, errors: Constants.INVALID_DATA);
                }

                BusinessModuleValidator validator = new BusinessModuleValidator();
                ValidationResult resultvalidation = validator.Validate(dto);
                if (!resultvalidation.IsValid)
                {
                    var resultDTO = _mapper.Map<List<ErrorResultDTO>>(resultvalidation.Errors);
                    return Response(statusCode: HttpStatusCode.BadRequest, hasErrors: true, errors: resultDTO);
                }

                var _entity = _mapper.Map<BusinessModule>(dto);
                //_entity.feCreacion = System.DateTime.Now;
                var result = await _service.PostAsync(_entity);

                return Response(_mapper.Map<BusinessModuleDTO>(result));

            }
            catch (Exception ex)
            {
                return Response(statusCode: HttpStatusCode.BadRequest, hasErrors: true, errors: ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] BusinessModuleDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    return Response(statusCode: HttpStatusCode.BadRequest, hasErrors: true, errors: Constants.INVALID_DATA);
                }

                BusinessModuleValidator validator = new BusinessModuleValidator();
                ValidationResult resultvalidation = validator.Validate(dto);
                if (!resultvalidation.IsValid)
                {
                    var resultDTO = _mapper.Map<List<ErrorResultDTO>>(resultvalidation.Errors);
                    return Response(statusCode: HttpStatusCode.BadRequest, hasErrors: true, errors: resultDTO);
                }

                var _entity = _mapper.Map<BusinessModule>(dto);
                //_entity.feCreacion = System.DateTime.Now;
                var result = await _service.PutAsync(_entity);

                return Response();
            }
            catch (Exception ex)
            {
                return Response(statusCode: HttpStatusCode.BadRequest, hasErrors: true, errors: ex);
            }
        }
    }
}
