using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.core.Interfaces.Repository;
using MISA.Core.Entities;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces.Service;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        ICustomerService _customerService;
        ICustomerRepository _customerRepository;

        public CustomerController(ICustomerService customerService, ICustomerRepository customerRepository)
        {
            _customerService = customerService;
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var customers = _customerRepository.GetAll();
            if (customers.Count() > 0)
            {
                return Ok(customers);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{customerId}")]
        public IActionResult GetById(Guid customerId)
        {
            Customer customer = _customerService.GetById(customerId);
            if (customer != null)
            {
                return Ok(customer);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            try
            {
                var rowAffect = _customerService.Insert(customer);
                if (rowAffect > 0)
                {
                    return StatusCode(201, rowAffect);
                }
                else
                {
                    return NoContent();
                }
            }
            catch(CustomException Ex)
            {
                var mes = new
                {
                    devMsg = Ex.Message,
                    userMsg = "Du lieu khong hop le vui long thu lai",
                    data = Ex.Data
                };
                return StatusCode(400, mes);
            }
            catch(Exception Ex)
            {
                var mes = new
                {
                    devMsg = Ex.Message,
                    userMsg = "Co loi xay ra vui long lien he MISA de duoc tro giup",
                };
                return StatusCode(500, mes);
            }
        }
    }
}
