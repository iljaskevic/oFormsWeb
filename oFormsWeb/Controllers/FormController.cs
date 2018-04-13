using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using oFormsWeb.Models;
using oFormsWeb.Repositories;

namespace oFormsWeb.Controllers
{
    [Authorize]
    public class FormController : BaseController
    {
        private readonly ILogger<FormController> _logger;
        private readonly IFormRepository _formRepository;
        private readonly IApiFormRepository _apiFormRepository;

        public FormController(ILogger<FormController> logger, IFormRepository formRepository, IApiFormRepository apiFormRepository)
        {
            _logger = logger;
            _formRepository = formRepository;
            _apiFormRepository = apiFormRepository;
        }

        // GET: Form
        public async Task<ActionResult> Index()
        {
            //IList<Form> forms = new List<Form>();
            //return View(forms);
            ViewBag.ObjectId = GetUserObjectId();
            IList<Form> forms = await _formRepository.GetAllClientForms(GetUserObjectId());
            return View(forms);
        }

        // GET: Form/Details/5
        public async Task<ActionResult> Details(string id)
        {
            //return View();
            Form form = await _formRepository.GetForm(GetUserObjectId(), id);
            //ViewData["Message"] = "Your contact page.";

            return View(form);
        }

        // GET: Form/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Form/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Form newForm)
        {
            _logger.LogDebug("Received Name: " + newForm.Name);
            _logger.LogDebug("Received Desc: " + newForm.Desc);
            try
            {
                // TODO: Add insert logic here
                //return RedirectToAction(nameof(Index));
                newForm.Id = Guid.NewGuid().ToString();
                newForm.ClientId = GetUserObjectId();
                newForm.Cors = "";
                newForm.ApiKey = Guid.NewGuid().ToString();
                newForm.FormTemplate = new FormTemplate(new MessageFormat());
                _formRepository.InsertOrUpdateForm(GetUserObjectId(), newForm);
                FormApiMap newFormApiMap = new FormApiMap();
                newFormApiMap.ApiKey = newForm.ApiKey;
                //newFormApiMap.ClientId = GetUserObjectId();
                //newFormApiMap.FormId = newForm.Id;
                //newFormApiMap.NumberOfRequests = 0;
                newFormApiMap.EmailInfo = newForm.FormTemplate;
                _apiFormRepository.InsertUpdateFormApiMap(GetUserObjectId(), newFormApiMap);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Form/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            _logger.LogDebug("View to edit ID: " + id);
            Form form = await _formRepository.GetForm(GetUserObjectId(), id);
            //ViewData["Message"] = "Your contact page.";

            return View(form);
        }

        // POST: Form/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Form editedForm)
        {
            _logger.LogDebug("Editing Name: " + editedForm.Name);
            _logger.LogDebug("Editing Desc: " + editedForm.Desc);
            try
            {
                // TODO: Add update logic here
                //return RedirectToAction(nameof(Index));
                _formRepository.InsertOrUpdateForm(GetUserObjectId(), editedForm);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Form/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            _logger.LogDebug("View to delete ID: " + id);
            Form form = await _formRepository.GetForm(GetUserObjectId(), id);
            return View(form);
        }

        // POST: Form/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, Form deletedForm)
        {
            _logger.LogDebug("Deleting ID: " + id);
            try
            {
                // TODO: Add delete logic here
                //return RedirectToAction(nameof(Index));
                _formRepository.DeleteForm(GetUserObjectId(), id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}