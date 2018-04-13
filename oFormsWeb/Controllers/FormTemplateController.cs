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
    public class FormTemplateController : BaseController
    {
        private readonly ILogger<FormTemplateController> _logger;
        private readonly IFormRepository _formRepository;
        private readonly IApiFormRepository _apiFormRepository;

        public FormTemplateController(ILogger<FormTemplateController> logger, IFormRepository formRepository, IApiFormRepository apiFormRepository)
        {
            _logger = logger;
            _formRepository = formRepository;
            _apiFormRepository = apiFormRepository;
        }

        // GET: FormTemplate
        public ActionResult Index()
        {
            //return View();
            return RedirectToAction(nameof(FormController.Index), "Form");
        }

        // GET: FormTemplate/Details/5
        public async Task<ActionResult> Details(string id)
        {
            //return View();
            Form form = await _formRepository.GetForm(GetUserObjectId(), id);
            ViewData["FormId"] = id;

            return View(form.FormTemplate);
        }

        // GET: FormTemplate/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            //return View();
            Form form = await _formRepository.GetForm(GetUserObjectId(), id);
            ViewData["FormId"] = id;

            return View(form.FormTemplate);
        }

        // POST: FormTemplate/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, FormTemplate editedFormTemplate)
        {
            try
            {
                //return View();
                Form editedForm = await _formRepository.GetForm(GetUserObjectId(), id);
                editedForm.FormTemplate = editedFormTemplate;
                _formRepository.InsertOrUpdateForm(GetUserObjectId(), editedForm);
                FormApiMap formApiMap = await _apiFormRepository.GetFormApiMap(editedForm.ApiKey);
                formApiMap.EmailInfo = editedFormTemplate;
                _apiFormRepository.InsertUpdateFormApiMap(GetUserObjectId(), formApiMap);
                ViewData["FormId"] = id;
                //return RedirectToAction(nameof(FormController.Index), "Form");
                return View(editedFormTemplate);
            }
            catch
            {
                return View();
            }
        }
    }
}