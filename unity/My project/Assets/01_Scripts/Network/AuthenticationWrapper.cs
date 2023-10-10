using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using AuthenticationException = System.Security.Authentication.AuthenticationException;

public enum AuthState
{
    NotAuthenticated,
    Authenticating,
    Authenticated,
    Error,
    TimeOut
}

public static class AuthenticationWrapper
{
    public static AuthState State { get; private set; } = AuthState.NotAuthenticated;
    public static event Action<string> OnMessageEvent;

    public static async Task<AuthState> DoAuth(int maxTries = 5)
    {
        if(State == AuthState.NotAuthenticated)
        {
            return State;
        }

        if(State == AuthState.Authenticated)
        {
            OnMessageEvent?.Invoke("인증이 진행중입니다");
            return await Authenticating();
        }
        await SignAnonymouslyAsync(maxTries);
        return State;
    }

    private static async Task SignAnonymouslyAsync(int maxTries)
    {
        State = AuthState.Authenticating;

        int tries = 0;
        while(State == AuthState.Authenticating && tries < maxTries)
        {
            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                if(AuthenticationService.Instance.IsSignedIn && AuthenticationService.Instance.IsAuthorized)
                {
                    State = AuthState.Authenticated;
                    break;
                }
            }
            catch(AuthenticationException ex)
            {
                OnMessageEvent?.Invoke(ex.Message);
                State = AuthState.Error;
                break;
            }
            catch(RequestFailedException ex)
            {
                OnMessageEvent?.Invoke(ex.Message);
                State = AuthState.Error;
                break;
            }

            ++tries;
            await Task.Delay(1000);
        }

        if(State != AuthState.Authenticated && tries == maxTries)
        {
            OnMessageEvent?.Invoke($"Auth timeout : {tries} tries");
            State = AuthState.TimeOut;
        }
    }

    private static async Task<AuthState> Authenticating()
    {
        while(State == AuthState.Authenticating)
        {
            await Task.Delay(200);
        }
        return State;
    }
}
