using UnityEngine;
using System.Collections;
using WWWKit;
using System.Collections.Generic;
using UnityEngine.Events;

namespace HTTP {
    public sealed class HHTPRequester : SingletonMonoBehaviour<HHTPRequester> {
        //private
        float timeout = 3.0f;
        WWWClient mClient;

        override protected void Awake() {
            if (this != Instance)
            {
                Destroy(this);
                return;
            }
            DontDestroyOnLoad(this);
        }

	    //-------------------------------------------------------------
	    // POSTリクエスト
	    // @param
	    // @リクエストURL
	    // @callback
	    //-------------------------------------------------------------
	    public void Post<T>(string url, Dictionary<string, string> post, UnityAction<T> callback) {
            mClient = new WWWClient(this);
            mClient.URL = url;
            foreach (KeyValuePair<string, string> post_arg in post)
            {
                mClient.AddData(post_arg.Key, post_arg.Value);
            }
            mClient.Timeout = timeout;
            mClient.OnDone = (WWW www) => { callback(JsonUtility.FromJson<T>(www.text)); };
            mClient.Request();
        }

        //-------------------------------------------------------------
        // GETリクエスト
        // @param
        // @リクエストURL
        // @callback
        // @brif POSTの時に使用したデータを消してやらないとPOSTだと判断されてしまう
        //-------------------------------------------------------------
        public void Get<T>(string url, UnityAction<T> callback) {
            mClient = new WWWClient(this);
            mClient.URL = url;
            mClient.Timeout = timeout;
            mClient.OnDone = (WWW www) => { callback(JsonUtility.FromJson<T>(www.text)); };
            mClient.Request();
        }
    }
}
