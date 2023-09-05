using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public enum AuthState
{
    NotAuthenticated,
    Authenticating,
    Authenticated,
    Error,
    TimeOut
}

public class AuthenticationWrapper
{
    public static AuthState State { get; private set; } = AuthState.NotAuthenticated;

    public static async Task<AuthState> DoAuth(int maxTries = 5)
    {
        if (State == AuthState.Authenticated)
        {
            return State;
        }

        if(State == AuthState.Authenticating)
        {
            Debug.LogWarning("Already authenticating!");
            State = await Authenticating();
            return State;
        }

        await SignInAnonymouslyAsync(maxTries); 

        return State;
    }

    private static async Task<AuthState> Authenticating()
    {
        while(State == AuthState.Authenticating || State == AuthState.NotAuthenticated)
        {
            await Task.Delay(200);
        }
        return State;
    }

    private static async Task SignInAnonymouslyAsync(int maxTries)
    {
        
        State = AuthState.Authenticating;
        int tries = 0;
        while (State == AuthState.Authenticating && tries < maxTries)
        {
            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                //지금 현재 로그인도 되었고, 승인도 끝난 상태인거
                if (AuthenticationService.Instance.IsSignedIn
                    && AuthenticationService.Instance.IsAuthorized)
                {
                    State = AuthState.Authenticated;
                    break;
                }
            }
            catch(AuthenticationException ex)
            {
                Debug.LogError(ex);
                State = AuthState.Error;
            }
            catch (RequestFailedException ex)
            {
                Debug.LogError(ex);
                State = AuthState.Error;
            }

            tries++;
            await Task.Delay(1000); //1초에 한번씩 인증시도
        }

        if(State != AuthState.Authenticated)
        {
            Debug.LogWarning($"Player was not signed in successfully after : {tries} tries");
            State = AuthState.TimeOut;
        }
    }
}
