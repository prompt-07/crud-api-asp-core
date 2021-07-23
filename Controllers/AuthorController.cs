using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenDotAPI.Models;

namespace TenDotAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : Controller
    {

        Hashtable employees = new Hashtable();

        [HttpGet]
        [Route("getApprovalCode/{contactCode}/{docValue}")]
        public string Get(string contactCode, int docValue)
        {
            using (var context = new EmployeeDBContext())
            {
                var empTDH = (from employee in context.UserRecord
                          where employee.ContactCode == contactCode
                          select employee.TenDotHirarchy).SingleOrDefault();

                var validApprovers = from approvalList in context.ApprovalMaster
                                     where docValue >= approvalList.LowerAmtLimit && docValue <= approvalList.UpperAmtLimit
                                     select approvalList;
                
                int counter = 0;
                int max;
                string approvalCode = "";
                foreach (var approver in validApprovers)
                {
                    max = counter;   
                    counter = customStringMatch(empTDH, approver.TenDotHirarchy);
                    if (counter == 10)
                    {
                        approvalCode = approver.Approver;
                        break;
                    }
                    else if(counter > max)
                    {
                        approvalCode = approver.Approver;
                    }
                }

                
                 return approvalCode;
                
                
            }

            int customStringMatch(string empTDH, string approverTDH)
            {
                int count = 0;
                for (int i = 0; i < 10; i++)
                {
                    if (empTDH[i] == approverTDH[i])
                        count++;

                    else
                        break;
                }
                return count;
            }
               
        }
    }
}
