using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleWeb.Models;

namespace SampleWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(UserInfoModels receiveModel)
        {
            int UserHeight_cm = receiveModel.UserHeight;
            double UserHeight_m = (double)receiveModel.UserHeight / 100;
            int UserWeight = receiveModel.UserWeight;

            int ResultTotalWater = GetTotalWater(UserHeight_cm, UserWeight);
            double ResultBMI = new ResultBMI() { UserHeight = UserHeight_m, UserWeight = UserWeight }.getResult();
            finalResult ResultObj = new finalResult() { 
                UserName=receiveModel.UserName,
                BMI = ResultBMI,
                totalWater = ResultTotalWater
            };
            ResultObj.discernSex(receiveModel.Sex);

            receiveModel.UserResult = ResultObj.getResult();

            return View(receiveModel);
        }

        public int GetTotalWater(int UserHeight,int UserWeight){
            return (UserHeight + UserWeight) * 10;
        }
    }

    public class ResultBMI : myResult<double>
    {
        public double UserHeight { get; set; }
        public int UserWeight { get; set; }
        public double getResult()
        {
            return this.UserWeight / (this.UserHeight * this.UserHeight);
        }
    }

    public class finalResult : myResult<string>
    {
        public string UserName { get; set; }
        public double BMI { get; set; }
        public int totalWater { get; set; }
        private string BMI_Msg { get; set; }
        private int SexTypeNum { get; set; }
        public string discernSex(int num)
        {
            this.SexTypeNum = num;
            string SexText = string.Empty;
            switch (num)
            {
                case 0:
                    SexText = "女性";
                    BMI_Check(18,22);//女性BMI 18~22
                    break;
                case 1:
                    SexText = "男性";
                    BMI_Check(20, 25);//男性BMI 20~25
                    break;
                default:
                    SexText = "男性";
                    break;
            }
            return SexText;
        }
        public void BMI_Check(int minMum,int maxMum)
        {
            this.BMI_Msg = "標準";
            if (this.BMI < minMum)
            {
                this.BMI_Msg = "過瘦";
            }
            if (this.BMI > maxMum)
            {
                this.BMI_Msg = "過胖";
            }
        }
        public string getResult()
        {
            string ResultText = string.Format("{0}，性別:{1}，BMI:{2}({3})，建議每日喝水{4}CC", this.UserName, this.discernSex(this.SexTypeNum).ToString(), this.BMI.ToString(), this.BMI_Msg, this.totalWater);
            return ResultText;
        }

    }
    public interface myResult<T>
    {
        T getResult();
    }


}