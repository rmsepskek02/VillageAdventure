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
using VillageAdventure;

public class AWSRank : MonoBehaviour
{
    CognitoAWSCredentials credentials;
    private void Awake()
    {
        UnityInitializer.AttachToGameObject(DataManager.Instance.gameObject);
        credentials = new CognitoAWSCredentials("ap-northeast-2:e187ee28-e179-4f6a-a4ca-426d2feb4c7b", RegionEndpoint.APNortheast2);
        //SetRank("RankLambda", "GET", "testNAme", 11639, null);
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

    public void SetRank(string func, string method, string name, int score, Action<string> onResponse)
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
                string responseBody = Encoding.ASCII.GetString(response.Response.Payload.ToArray());
                //Debug.Log(responseBody);
                Debug.Log("Suc");
                onResponse?.Invoke(responseBody); // 콜백 함수 호출하여 응답 전달
        }
            else
            {
                Debug.Log(response.Exception);
                Debug.Log("Fail");
                onResponse?.Invoke(null); // 실패 시에도 콜백 함수 호출 (null 전달)
        }
        });
    }
}
