﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using Microsoft.Win32;
using System.Text;
using System.Text.Json;
using UniversityApp.UI.Exceptions;
using UniversityApp.UI.Filters;
using UniversityApp.UI.Models;
using UniversityApp.UI.Services;

namespace UniversityApp.UI.Controllers
{
    [ServiceFilter(typeof(AuthFilter))]
    public class GroupController : Controller
    {
        private HttpClient _client;
        private readonly ICrudService _crudService;

        public GroupController(ICrudService crudService)
        {
            _client = new HttpClient();
            _crudService = crudService;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            try
            {
                return View(await _crudService.GetAllPaginated<GroupListItemDetailedGetResponse>("groups", page));
            }
            catch (HttpException e)
            {
                if(e.Status == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("login", "auth");
                }
                else return RedirectToAction("error", "home");
            }
            catch(Exception e)
            {
                return RedirectToAction("error", "home");
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GroupCreateRequest createRequest)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                await _crudService.Create<GroupCreateRequest>(createRequest, "groups");
                return RedirectToAction("index");
            }
            catch (ModelException e)
            {
                foreach (var item in e.Error.Errors)  ModelState.AddModelError(item.Key, item.Message);
                return View();
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View(await _crudService.Get<GroupCreateRequest>("groups/" + id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GroupCreateRequest editRequest, int id)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                await _crudService.Update<GroupCreateRequest>(editRequest, "groups/"+id);
                return RedirectToAction("index");
            }
            catch (ModelException e)
            {
                    foreach (var item in e.Error.Errors)
                        ModelState.AddModelError(item.Key, item.Message);

                    return View();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _crudService.Delete("groups/" + id);
                return Ok();
            }
            catch(HttpException e)
            {
                return StatusCode((int)e.Status);
            }
        }
    }
}
