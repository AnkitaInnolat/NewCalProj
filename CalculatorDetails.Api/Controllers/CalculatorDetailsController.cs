using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using CalculatorDetails.data;

namespace CalculatorDetails.Api.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using CalculatorDetails.data;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<CalculatorDetail>("CalculatorDetails");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class CalculatorDetailsController : ODataController
    {
        private AngularCalculatorEntities db = new AngularCalculatorEntities();

        // GET: odata/CalculatorDetails
        [EnableQuery]
        public IQueryable<CalculatorDetail> GetCalculatorDetails()
        {
            return db.CalculatorDetails;
        }

        // GET: odata/CalculatorDetails(5)
        [EnableQuery]
        public SingleResult<CalculatorDetail> GetCalculatorDetail([FromODataUri] int key)
        {
            return SingleResult.Create(db.CalculatorDetails.Where(calculatorDetail => calculatorDetail.id == key));
        }

        // PUT: odata/CalculatorDetails(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<CalculatorDetail> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CalculatorDetail calculatorDetail = db.CalculatorDetails.Find(key);
            if (calculatorDetail == null)
            {
                return NotFound();
            }

            patch.Put(calculatorDetail);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalculatorDetailExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(calculatorDetail);
        }

        // POST: odata/CalculatorDetails
        public IHttpActionResult Post(CalculatorDetail calculatorDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CalculatorDetails.Add(calculatorDetail);
            db.SaveChanges();

            return Created(calculatorDetail);
        }

        // PATCH: odata/CalculatorDetails(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<CalculatorDetail> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CalculatorDetail calculatorDetail = db.CalculatorDetails.Find(key);
            if (calculatorDetail == null)
            {
                return NotFound();
            }

            patch.Patch(calculatorDetail);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalculatorDetailExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(calculatorDetail);
        }

        // DELETE: odata/CalculatorDetails(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            CalculatorDetail calculatorDetail = db.CalculatorDetails.Find(key);
            if (calculatorDetail == null)
            {
                return NotFound();
            }

            db.CalculatorDetails.Remove(calculatorDetail);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CalculatorDetailExists(int key)
        {
            return db.CalculatorDetails.Count(e => e.id == key) > 0;
        }
    }
}
