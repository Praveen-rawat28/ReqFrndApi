using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ReqFrndApi.Models;
using System.Linq;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class FriendRequestController : ControllerBase
{
    private readonly AppDbContext _context;

    public FriendRequestController(AppDbContext context)
    {
        _context = context;
    }
    [HttpPost("TakeAction")]
    public IActionResult TakeActionOnFriendRequest(ManageFriend manageFriend)
    {
        ResponseModel model = new ResponseModel();
        var existingRequest = _context.FriendRequest
        .FirstOrDefault(fr => fr.SenderId == manageFriend.SenderId && fr.ReceiverId == manageFriend.RecevierId && fr.Action == "Pending");

        var request = _context.FriendRequest.FirstOrDefault(x => x.SenderId == manageFriend.SenderId && x.ReceiverId == manageFriend.RecevierId);

        
        if (request != null)
        {
            request.Action = manageFriend.Action;
            _context.SaveChanges();
            
            model.DateTime = DateTime.Now;
            model.ResponseCode = 200;
            model.ResponseMessage = "friend request " + manageFriend.Action + " successfully";
           
            model.Data = manageFriend;
        }
      


        return Ok(model);


    }



    



    [HttpPost("sendreq")]
    public IActionResult SendFriendReq(int senderId, int receiverId)

    {



        if (senderId == receiverId)
        {
            return BadRequest("Sender and receiver cannot be the same.");
        }

        //var existingRequest = _context.FriendRequest
        // .FirstOrDefault(fr => fr.ReceiverId == senderId && fr.SenderId == receiverId || fr.Action == "Rejected");

        //if (existingRequest != null)
        //{
        //    return Conflict("A friend request has already send by user.");
        //}

        //var request = _context.FriendRequest.FirstOrDefault(fr => fr.SenderId == senderId && fr.ReceiverId == receiverId && fr.Action == "Pending" || fr.SenderId == senderId && fr.ReceiverId == receiverId && fr.Action == "Accepted" ||
        //fr.ReceiverId == senderId && fr.SenderId == receiverId);
        var request = _context.FriendRequest.FirstOrDefault(fr =>
    (fr.SenderId == senderId && fr.ReceiverId == receiverId && (fr.Action == "Pending" || fr.Action == "Accepted")) ||
    (fr.ReceiverId == senderId && fr.SenderId == receiverId));
        if (request != null)
        {
            return Conflict("A friend request has already been sent to this user and is still pending or already accepted.");
        }

        var friendRequest = new FriendRequest
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Action = "Pending",
                DateTime = DateTime.Now
                

            };

            _context.FriendRequest.Add(friendRequest);
        
            _context.SaveChanges();

            return Ok(friendRequest);
        
        
    }






    [HttpGet("CheakList")]
    public IActionResult GetFriendRequests(int id)
    {
        // Fetch friend requests where the recipient is the specified user and the request is not accepted
        var friendRequests = _context.FriendRequest
            .Where(x => x.ReceiverId == id)
            .ToList();

        // Return the list of friend requests
        return Ok(friendRequests);
    }


  
    [HttpGet("CheckMutualFrnds")]
    public IActionResult GetMutualFriends(int who_want_to_Know, int which_one)
    {
        // Check if both users exist
        var sender = _context.Users.FirstOrDefault(x => x.Id == who_want_to_Know);
        var receiver = _context.Users.FirstOrDefault(x => x.Id == which_one);

        if (sender == null || receiver == null)
        {
            return BadRequest("One or both users do not exist.");
        }
            var senderFriends = _context.FriendRequest
        .Where(f => f.SenderId == who_want_to_Know && f.Action == "Accepted" || f.ReceiverId== which_one && f.Action == "Accepted")
        .Select(f => f.ReceiverId) // Assuming the receiver is the friend
        .ToList();

        // Get the list of friends for the receiver
        var receiverFriends = _context.FriendRequest
            .Where(f => f.ReceiverId == which_one && f.Action == "Accepted" || f.SenderId== who_want_to_Know && f.Action == "Accepted")
            .Select(f => f.SenderId) // Assuming the receiver is the friend
            .ToList();
            var mutualFriendIds = senderFriends.Intersect(receiverFriends).ToList();


        var mutualFriendDetails = _context.Users
       .Where(u => mutualFriendIds.Contains(u.Id))
       .Select(u => new
       {
           u.Id,
           u.Username // Assuming you have a Name property in the Users table
       })
       .ToList();

        // Return the list of mutual friends, even if it's empty
        return Ok(mutualFriendDetails);



        
    }
    [HttpPost("search ")]
     public IActionResult Search(string name)
    {
        //var get= _context.Users.ToList();
        var find = _context.Users.Where(u => u.Username == name);
        return Ok(find);
    }
}



