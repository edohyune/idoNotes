         
        - 지도에서 영역선택
            1. **My Maps 활용**
                
                [Link : MyMap](https://www.google.com/maps/about/mymaps/)
                
                KML파일의 생성 위치를 지정했을때 위치값과 KML파일에 생성된 영역의 이름(프랜차이지 점포 이름)을 리턴할 수 있으면 활용 가능
                
            2. **Google Maps API를 활용:**
                
                - API 키 준비:
                    
                    먼저 Google Cloud Platform에서 API 키를 얻어야 합니다.
                    
                    Google Cloud Console에 접속합니다.
                    
                    새 프로젝트를 생성합니다.
                    
                    "API 및 서비스" > "라이브러리"로 이동하여 "Maps JavaScript API"를 검색하고 활성화합니다.
                    
                    "API 및 서비스" > "사용자 인증 정보"에서 "API 키"를 생성합니다.
                    
                - 웹사이트에 Google Maps 삽입:
                    
                    ```SQL
                    <!DOCTYPE html>
                    <html>
                    <head>
                      <title>Simple Google Maps</title>
                    </head>
                    <body>
                    <div id="map" style="height: 400px; width: 100%;"></div>
                    
                    <script async defer
                      src="https://maps.googleapis.com/maps/api/js?key=YOUR_API_KEY&callback=initMap">
                    </script>
                    <script>
                      var map;
                      function initMap() {
                        map = new google.maps.Map(document.getElementById('map'), {
                          center: {lat: -34.397, lng: 150.644},
                          zoom: 8
                        });
                      }
                    </script>
                    
                    </body>
                    </html>
                    ```
                    
                    주요 기능:  
                    마커 추가: 특정 위치에 마커를 추가하여 위치를 표시할 수 있습니다.  
                    다양한 오버레이: 다각형, 원, 선 등의 오버레이를 지도에 추가할 수 있습니다.  
                    이벤트: 사용자의 행동(예: 클릭, 드래그)에 반응하는 이벤트 리스너를 추가할 수 있습니다.  
                    지오코딩: 주소를 위도와 경도로 변환하거나, 반대로 위도와 경도를 주소로 변환하는 기능입니다.  
                    방향 및 경로: 두 지점 사이의 경로나 길찾기를 제공합니다.  
                    
                - 제3의 앱/서비스 활용:
                    
                    몇몇 웹 서비스나 앱에서는 구글 맵을 기반으로 추가적인 기능을 제공하기도 합니다. 이러한 서비스를 활용하여 원하는 영역 정보를 얻을 수도 있습니다.
                    
                
                [[권역 입력형 지도 개발]]
                
