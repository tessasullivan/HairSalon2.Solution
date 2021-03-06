using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class ClientsController : Controller
  {
    [HttpGet("/clients")]
    public ActionResult Index()
    {
      List<Client> allClients = Client.GetAll();
      return View(allClients);
    }

    //Displays form to allow user to enter client information
    [HttpGet("/stylists/{stylistId}/clients/new")]
    public ActionResult New(int stylistId)
    {
      Stylist stylist = Stylist.Find(stylistId);
      return View(stylist);
    }

    //Displays edit form
    [HttpGet("/stylists/{stylistId}/clients/{clientId}/edit")]
    public ActionResult Edit(int stylistId, int clientId)
    {
      Client client = Client.Find(clientId);
      return View(client);
    }

    //Edits client information
    [HttpPost("/stylists/{stylistId}/clients/{clientId}")]
    public ActionResult Update(int stylistId, int clientId, string firstName, string lastName, string phoneNumber, string notes)
    {
      Client client = Client.Find(clientId);
      client.Edit(firstName, lastName, phoneNumber, stylistId, notes);
      return View("Show", client);
    }

    [HttpGet("stylists/{stylistId}/clients/{clientId}")]
    public ActionResult Show(int clientId)
    {
      Client client = Client.Find(clientId);
      return View(client);
    }
    // Ask for confirmation to delete client
    [HttpGet("/stylists/{stylist_id}/clients/{client_id}/delete")]
    public ActionResult Delete(int stylist_id, int client_id)
    {
      Client client = Client.Find(client_id);
      return View(client);
    }

    // Delete an individual client 
    [HttpPost("/stylists/{stylist_id}/clients/{client_id}/delete")]
    public ActionResult DeleteStylist(int stylist_id, int client_id)
    {
      Client client = Client.Find(client_id);
      client.Delete();
      List<Client> allClients = Client.GetAll();
      return View("Index", allClients);
    }

    // Ask for confirmation to delete all stylists and clients
    [HttpGet("/clients/delete")]
    public ActionResult DeleteAll()
    {
      return View();
    }
    [HttpPost("/clients/delete")]
    public ActionResult DeleteEveryOne()
    {
      Client.DeleteAll();
      List<Stylist> allStylists = Stylist.GetAll();
      return RedirectToAction("Index","Stylists");
    }
  }
}

