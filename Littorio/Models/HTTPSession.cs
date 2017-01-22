using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Littorio.Models
{
    public class HTTPSession
    {
        private UInt64 no; // 세션의 번호를 지정 
        private string nethost; // 실제 호스트 명을 기록, 이는 헤더의 Origin 과 상이할 수 있다. 
        private string netport; // 실제 포트 정보를 기록한다, 이는 url 에 존재할 수 도 그렇지 않을 수도 있다. 
        /*
            예시: 
            www.example.com:9090
            URL : www.example.com
            연결 주소 : 123.123.123.123:9090 

            URL : www.example.com:9090
            연결 주소 : 123.123.123.123:9090

            즉, URL 에 포트정보가 기입되지 않더라도 연결되는 포트가 지정될 수 있다. 

            그렇다면 Browser를 통해서 이러한 방식으로 연결될 가능성이 존재하는가? 

            아니다. 

            따라서, Browser 를 매개로 하여 capture를 하기때문에, 실제로는 URL 상에 포트가 표기되는 일 없이

            실제 Port 가 다를 수 없다. 그러므로 우리는 Capture 되는 URL 상의 포트정보를 신뢰한다. 

            나중에 따로 연결을 수립하는 경우 TCP 포트를 신경쓰기로 한다. (Burpsuite 의 Repeater 처럼)

            
        */
        private string method; // HTTP Request Method 이다. 
        private bool is_https; // HTTPS 인지 여부를 표기

        private Dictionary<string, string> headers; // HTTP 헤더를 저장 (키, 값)
        private Dictionary<string, string> parameters; // HTTP 파라미터를 저장 (키, 값)
        private Dictionary<string, bool> resultTest; // 테스트 결과를 저장

        // 리스폰스의 정보 또한 저장되어야 하는 것이 아닌가? 
        private bool hasResponse; // Response 의 정보가 존재하는 경우와 그렇지 않은 경우 명시 

        // hasResponse 필드의 존재가 정당하려면 aftersessioncomplete 만의 구현으로는 힘들다. 

        // 예시, 요청을 전달하지만 서버의 상황으로 인해 응답이 도착하지 않거나 지연되는 경우가 발생한다.
        // 지연되는 경우는 aftersessioncomplete 이벤트 핸들러를 통해서 해결이 가능하지만, 응답이 도착하지 않는 경우를 
        // 체크하기는 힘든것이 사실이다. 

        // 따라서, 다음의 전략이 필요로하다. 
        /*
         OnRequest 발생 시에 우선 Request에 대한 정보를 저장해둔다. 그리고 세션 번호를 따로 저장해 두어서 
         Dictionary 에 저장한다. 해당 Dictionary 는 미완의 정보를 저장하는 개념이다. (일종의 버퍼랄까?)
         미완의 정보가 많이 쌓이면 이를 초기화해주어야 하는데,,,, 이를 어떻게 구분할 수 있을까? 
         Response 가 도착할 경우 Fiddler 코어 내부에서는 이를 구분할 수 있을 것이다. 그렇다면 Response 도착시에도 
         전달되는 객체의 정보상에 미리 분류된 Session Number 가 붙어 있을터 이를 이용해서 구분하면 쉬운일이 될 것이다. 
         그리고 hasResponse 객체에 마킹을 하고 
         나머지 정보들을 채워나간다. 
         */

    }
}
