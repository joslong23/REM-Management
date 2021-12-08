﻿using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Controllers
{

    [Route("properties")] // properties

    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyDAO properties;

        public PropertyController(IPropertyDAO properties)
        {
            this.properties = properties;
        }


        [HttpPost]
        [Authorize ("admin")]
        public ActionResult AddProperty(Property property)
        {

            int userId = int.Parse(this.User.FindFirst("sub").Value);


            Property addedProperty = properties.AddProperty(userId, property);

            return Created("/properties/" + addedProperty.Id, addedProperty);

        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetAllProperties()
        {
            IEnumerable<Property> results = properties.GetAllProperties();

            return Ok(results);
        }



    }
}