﻿using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeContext _context;

        public EmployeeController(EmployeeContext context)
        {
            this._context = context;
        }

        // Read
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> Get()
        {
            return Ok(await _context.Employee.ToListAsync());
        }

        // Read one
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> Get(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null) { 
                return BadRequest(404);
            }
            else
            {
                return Ok(employee);
            }
        }

        // Add
        [HttpPost]
        public async Task<ActionResult<List<Employee>>> Add(Employee employee)
        {   
            _context.Add(employee);
            await _context.SaveChangesAsync();
            return Ok(await _context.Employee.ToListAsync());
        }

        // Update
        [HttpPut]
        public async Task<ActionResult<List<Employee>>> Update(Employee request)
        {
            var employee = await _context.Employee.FindAsync(request.Id);
            if (employee == null)
            {
                return BadRequest(404);
            }
            else
            {
                employee.Name = request.Name;
                employee.Department = request.Department;
                employee.Position = request.Position;
                employee.Sex = request.Sex;
                employee.Experience = request.Experience;
                employee.Salary = request.Salary;

                await _context.SaveChangesAsync();

                return Ok(employee);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<List<Employee>>> Delete(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return BadRequest(404);
            }
            else
            {
                _context.Employee.Remove(employee);

                await _context.SaveChangesAsync();

                return Ok(employee);
            }
        }
    }
}
