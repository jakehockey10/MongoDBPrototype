﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDBPrototype
{
    public class ConsoleOutputter
    {
        public static void OutputResultsOf(Action action)
        {
            Console.WriteLine(TaskCompletionTimeRecorder.RecordTaskCompletionTime(action));
        }

        public static async Task OutputResultsOf(Func<Task<int>> action)
        {
            Console.WriteLine(await TaskCompletionTimeRecorder.RecordTaskCompletionTime(async () => await action()));
        }

        public static async Task OutputResultsOf(Func<Task<List<BsonDocument>>> action)
        {
            Console.WriteLine(await TaskCompletionTimeRecorder.RecordTaskCompletionTime(async () => await action()));
        }

        public static async Task OutputResultsOf(Func<Task<UpdateResult>> action)
        {
            Console.WriteLine(await TaskCompletionTimeRecorder.RecordTaskCompletionTime(async () => await action()));
        }
    }
}