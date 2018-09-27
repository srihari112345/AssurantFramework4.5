using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employee.Api.DTO;
using Employee.Domain;
using System.Web.Http;
using System.Web;
using System.Net.Http;
using System.Net;


namespace Employee.Api.Controllers
{
    [Route("api/employee")]
    public class EmployeeController : ApiController
    {
        private EmployeeDbContext _dbcontext;

        public EmployeeController(EmployeeDbContext dbcontext)
        {
            this._dbcontext = dbcontext;
        }

        [Route("zero/project")]
        [HttpGet]
        public IHttpActionResult NoProject()
        {
            List<EmployeeDTO> employeeDTO = new List<EmployeeDTO>();
            try
            {
                var employees = _dbcontext.Employees.Where(x => !x.ProjectEngagements.Select(y => y.EmployeeId)
                                                                                     .Contains(x.EmployeeId))
                                                          .ToList();

                employees.ForEach(x =>
                {
                    employeeDTO.Add(new EmployeeDTO
                    {
                        EmployeeId = x.EmployeeId,
                        Name = x.Name,
                        BaseLocation = x.BaseLocation

                    });
                });

                return Ok(employeeDTO);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        [Route("single/project")]
        [HttpGet]
        public IHttpActionResult SingleProject()
        {
            List<EmployeeDTO> employeeDTO = new List<EmployeeDTO>();
            try
            {
                var employees = _dbcontext.Employees.Where(x => x.ProjectEngagements.Where(y => y.EmployeeId == x.EmployeeId)
                                                                                                  .Count()
                                                                                                  .Equals(1))
                                                          .ToList();

                employees.ForEach(x =>
                {
                    employeeDTO.Add(
                        new EmployeeDTO
                        {
                            EmployeeId = x.EmployeeId,
                            Name = x.Name,
                            BaseLocation = x.BaseLocation
                        });
                });
                return Ok(employeeDTO);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        [Route("multiple/projects")]
        [HttpGet]
        public IHttpActionResult MultipleProjects()
        {
            List<EmployeeDTO> employeeDTO = new List<EmployeeDTO>();
            try
            {
                var employees = _dbcontext.Employees.Where(x => x.ProjectEngagements.Where(y => y.EmployeeId == x.EmployeeId)
                                                                                    .Count() > 1)
                                                    .ToList();
                employees.ForEach(x =>
                {

                    employeeDTO.Add(
                        new EmployeeDTO
                        {
                            EmployeeId = x.EmployeeId,
                            Name = x.Name,
                            BaseLocation = x.BaseLocation
                        });
                });
                return Ok(employeeDTO);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }
    }
}