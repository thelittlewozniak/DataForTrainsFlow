using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainsFlow.Models
{
    public class TrainPrediction
    {
        [ColumnName("PredictedDelay")]
        public float PredictedTimes { get; set; }
    }
}
