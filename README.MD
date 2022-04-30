# How to use

need ScriptLoader.dll https://github.com/ghorsington/BepInEx.ScriptLoader  
Put *.cs file of this project in COM3D2\scripts form https://github.com/customordermaid3d2/COM3D2.ScriptLoader.Script/tree/master/COM3D2.ScriptLoader.Script  
  
ScriptLoader.dll 필요 https://github.com/ghorsington/BepInEx.ScriptLoader  
https://github.com/customordermaid3d2/COM3D2.ScriptLoader.Script/tree/master/COM3D2.ScriptLoader.Script 에서 *.cs 파일을 COM3D2\scripts 에 넣기  
내가 개발한것 외에 CMI나 디스코드의 파일도 포함됨


# add_subs_to_old_yotogi - CMI  


# all_maids_in_private_mode - CMI 

프라이빗 모드에 모든 메이드 표시  
PrivateMaidList와 같으니 둘중 하나만 쓰기

# CharacterEditSortRedux - CMI 

메이드 정렬  
물리적 정렬이 아닌 표시를 정렬함  
CharacterSelectManagerSort와 중복되니 둘중 하나만 쓸것  


# CharacterSelectManagerSort - Lilly   

메이드 정렬  
물리적 정렬이 아닌 표시를 정렬함  
CharacterEditSortRedux와 중복되니 둘중 하나만 쓸것  


# dump_game_info - CMI

게임정보 출력용  
디스코드에 분석 의뢰시 이스크립트를 포함해서 실행 후 
COM3D2\BepinEx\LogOutput.log  파일을 공유할것  


# editable_names - CMI 

메이드 편집시 이름 수정 가능  


# editBlinkStop - CMI


# enable_scout_mode - CMI

스카웃 메뉴 항상 활성화  


# fastFade - CMI  

fastFade플러그인의 스크립트 버전  
기존 플러그인과 다르게 날자 전환은 스킾 안함  


# ForceScheduleEvents - discode

https://discordapp.com/channels/297072643797155840/736350853442699284/956495568887226398  
스케줄 강제 활성화  
static readonly KeyCode TOGGLE_KEYCODE = KeyCode.E;  
부분으로 설정 수정 가능


# GuestModeFree - Lilly

게스트모드의 플레이 버튼 모두 활성화  


# keyChange

https://youtu.be/twhuHH77T7s  
대사 진행창에서 앤터를 눌러야만 진행되는데  
스페이스로도 진행할수 있게됨.  
스크립트 수정해서 다른키로 변경 및 추가 가능  
return Input.GetKeyDown(keyCode) || Input.GetKeyDown(KeyCode.Space);  


# MemoriesModeUnlock - Lilly

회상모드의 일상,VIP 메뉴 활성화  


# MotionWindowAddMyPose - Lilly

스튜디오모드에서 포즈 저장시 포즈 목록 자동 갱신  


# nameExtender - CMI


# NoMessageBox - Lilly

메시지창을 안뜨게하고 대신 LogError로 찍힘  


# PrivateEventListEnable - Lilly

프라이빗모드에서 모든 메뉴 활성화  
all_maids_in_private_mode와 같으니 둘중 하나만 쓰기  


# report_dupes - CMI

중복파일목록을 menu_dupes.log로 출력  


# saveSettingsInGame - CMI

saveSettingsInGame 플러그인의 스크립트 버전  


# SceneFreeModeSelectManagerYotogi - Lilly

밤시중 횟수가 0인경우 회상 밤시중 사용불가.  
횟수 무시하고 사용 가능하게 설정  


# skipStartLogo - CMI

fastFade플러그인의 스크립트 버전 스타트로고만 스킾됨  


# unlock_all_skills - CMI

스킬 강제로 배우기  
메이드 관리에서 아래키 누르면 실행됨  
static readonly KeyCode UNLOCK_KEYCODE = KeyCode.U;  
static readonly KeyCode RESET_KEYCODE = KeyCode.R;  
