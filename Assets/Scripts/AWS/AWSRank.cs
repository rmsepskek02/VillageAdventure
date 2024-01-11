using Amazon;
using Amazon.CognitoIdentity;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Lambda;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class AWSRank : MonoBehaviour
{
    CognitoAWSCredentials credentials;
    private void Awake()
    {
        UnityInitializer.AttachToGameObject(this.gameObject);
        credentials = new CognitoAWSCredentials("ap-northeast-2:e187ee28-e179-4f6a-a4ca-426d2feb4c7b", RegionEndpoint.APNortheast2);
        SetRank("RankLambda", "DELETE", "1testNAme", 11637);
    }
    IAmazonLambda _lambda;
    IAmazonLambda LambdaClient
    {
        get
        {
            if (_lambda == null)
            {
                _lambda = new AmazonLambdaClient(credentials, RegionEndpoint.APNortheast2);
            }
            return _lambda;
        }
    }

    public void SetRank(string func, string method, string name, int score)
    {
        LambdaClient.InvokeAsync(new Amazon.Lambda.Model.InvokeRequest()
        {
            FunctionName = func,
            Payload = "{\"Method\":" + $"\"{method}\"," + "\"name\":" + $"\"{name}\"," + "\"score\":" + $"{score}" + "}"
        },
        (response) =>
        {
            if (response.Exception == null)
            {
                Debug.Log(Encoding.ASCII.GetString(response.Response.Payload.ToArray()));
                Debug.Log("Suc");
            }
            else
            {
                Debug.Log(response.Exception);
                Debug.Log("Fail");
            }
        });
    }
}
