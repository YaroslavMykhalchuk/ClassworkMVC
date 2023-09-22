using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAppApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalcController : ControllerBase
    {
        [HttpGet("add")]
        public string GetAdd(string a, string b)
        {
            int firstNum, secondNum;

            if (int.TryParse(a, out firstNum))
            {
                if (int.TryParse(b, out secondNum))
                    return $"{firstNum}+{secondNum}={firstNum + secondNum}";
                else
                    return "Change b";
            }
            else
                return "Change a";
        }

        [HttpGet("sub")]
        public string GetSub(string a, string b)
        {
            int firstNum, secondNum;

            if (int.TryParse(a, out firstNum))
            {
                if(int.TryParse(b, out secondNum))
                    return $"{firstNum}-{secondNum}={firstNum - secondNum}";
                else
                    return "Change b";
            }
            else
                return "Change a";
        }

        [HttpGet("mul")]
        public string GetMultiplication(string a, string b)
        {
            int firstNum, secondNum;

            if (int.TryParse(a, out firstNum))
            {
                if (int.TryParse(b, out secondNum))
                    return $"{firstNum}*{secondNum}={firstNum * secondNum}";
                else
                    return "Change b";
            }
            else
                return "Change a";
        }

        [HttpGet("div")]
        public string GetDivision(string a, string b)
        {
            int firstNum, secondNum;

            if (int.TryParse(a, out firstNum))
            {
                if (int.TryParse(b, out secondNum))
                {
                    if (secondNum == 0)
                        return "You can't division by 0";
                    return $"{firstNum}/{secondNum}={firstNum / secondNum}";
                }
                else
                    return "Change b";
            }
            else
                return "Change a";
        }
    }
}
