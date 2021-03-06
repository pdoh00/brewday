﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using brewday.calculations;

namespace brewday.resource.endpoint.Controllers
{
    [Authorize]
    [RoutePrefix("api/calculations")]
    public class CalculationsController : ApiController
    {
        [HttpGet]
        [Route("srmtol")]
        public double SRMtoLovibond(double srm)
        {
            return Functions.lovibond(srm);
        }

        [HttpGet]
        [Route("lovitosrm")]
        public double LovibondtoSRM(double lovibond)
        {
            return Functions.srm(lovibond);
        }

        [HttpGet]
        [Route("srmtoebc")]
        public double SRMtoEBC(double srm)
        {
            return Functions.ebc(srm);
        }

        [HttpGet]
        [Route("abv")]
        public double ABV(double og, double fg)
        {
            return Functions.abv(og, fg);
        }

        [HttpGet]
        [Route("ibu")]
        public double IBU(double aau, double utilization, double wortVolume)
        {
            return Functions.IBU(aau, utilization, wortVolume);
        }

        [HttpGet]
        [Route("aau")]
        public double AAU(double hopWeight, double alphaAcidPercentage)
        {
            return Functions.AAU(hopWeight, alphaAcidPercentage);
        }

        [HttpGet]
        [Route("ph")]
        public double pH(double alkPPM, double caPPM, double mgPPM)
        {
            return Functions.pH(alkPPM, caPPM, mgPPM);
        }

        [HttpGet]
        [Route("totalwaterrequired")]
        public double RequiredWater(double targetBatchSize, double estTrubLoss, double grainWeight, double boilTime, double estLossToEquipment)
        {
            return Functions.requiredWater(targetBatchSize, estTrubLoss, grainWeight,  boilTime, estLossToEquipment);
        }

        [HttpGet]
        [Route("hoputilization")]
        public double HopUtilization(double boilGravity, double boilTime)
        {
            return Functions.HopUtilization(boilGravity, boilTime);
        }

        /// <summary>
        /// Calculates the amount of extract (in lbs) to hit desired total gravity
        /// </summary>
        /// <param name="totalGravityTarget">In gravity units</param>
        /// <param name="totalGravityMash">In gravity units</param>
        /// <param name="extractGUContribution">GU contribution of extract</param>
        /// <returns>Amount of extract in lbs</returns>
        [HttpGet]
        [Route("totalextractrequired")]
        public double TotalExtractRequired(double totalGravityTarget, double totalGravityMash, double extractGUContribution)
        {
            return Functions.Extractlbs(totalGravityTarget, totalGravityMash, extractGUContribution);
        }
    }
}
