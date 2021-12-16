#include <M5Core2.h>

int screenstate = 0; 

//keypad needed declarations 
int n=3;
int n2=4;

#define light 0xCF3E

int posX[4];

float numbers;
int boxSize=45;
int space=3;
int start=25;
String math="";

char buttons[3][4]={{'7','4','1','0'},{'8','5','2','.'},{'9','6','3',','}};
int pres=1;

void reset(){
  math="";
  M5.Lcd.fillRoundRect(posX[0],3,140,40,4,BLACK);
  M5.Lcd.drawString(math,160,40,4);
  }

 //speed needed declarations 
ButtonColors on_clrs = {BLACK, WHITE, WHITE};
ButtonColors off_clrs = {BLACK, WHITE, WHITE};

Button tl(0, 0, 0, 0, true,"1", off_clrs, on_clrs,TL_DATUM);
Button bl(0, 0, 0, 0, true, "3", off_clrs, on_clrs,BL_DATUM);
Button tr(0, 0, 0, 0, true, "2", off_clrs, on_clrs, TR_DATUM);
Button br(0, 0, 0, 0, true, "4", off_clrs, on_clrs, BR_DATUM);

float MET;

void doButtons() {
  int16_t hw = M5.Lcd.width() / 2;
  int16_t hh = M5.Lcd.height() / 2;
  tl.set(0, 0, hw - 5, hh - 5);
  bl.set(0, hh + 5, hw - 5, hh - 5);
  tr.set(hw + 5, 0, hw - 5, hh - 5);
  br.set(hw + 5, hh + 5, hw - 5, hh - 5);
  M5.Buttons.draw();
}

void switchColor(Event& e) {
  Button& b = *e.button;

  if (b != M5.background) {
    b.off.bg = (b.off.bg == BLACK) ? DARKGREEN : BLACK;
    b.draw();
  }
}

void touchevent1 (Event& e) {
  MET=5;
}

void touchevent2 (Event& e) {
  MET=6.3;
}

void touchevent3 (Event& e) {
  MET=8.8;
}

void touchevent4 (Event& e) {
  MET=11.5;
}

//stopwatch needed declarations 
volatile unsigned long usecCount = 0;
hw_timer_t *interrupptTimer = NULL;
portMUX_TYPE mutex = portMUX_INITIALIZER_UNLOCKED;

int display[3] = {0};

int countStart = 0;
int startCheck = 1;

void IRAM_ATTR usecTimer()
{
  portENTER_CRITICAL_ISR(&mutex);
  usecCount += 5;
  portEXIT_CRITICAL_ISR(&mutex);
}

void Introduction(){
  M5.Lcd.fillScreen(BLACK);

  M5.Lcd.fillEllipse(100,130,18,55,DARKGREEN);
  delay(500);
  M5.Lcd.fillEllipse(120,130,18,65,DARKGREEN);
  delay(500);
  M5.Lcd.fillEllipse(140,130,18,70,DARKGREEN);
  delay(500);
  M5.Lcd.fillEllipse(160,130,18,75,DARKGREEN);
  delay(500);
  M5.Lcd.fillEllipse(180,130,18,70,DARKGREEN);
  delay(500);
  M5.Lcd.fillEllipse(200,130,18,65,DARKGREEN);
  delay(500);
  M5.Lcd.fillEllipse(220,130,18,55,DARKGREEN);
  delay(500);
  
  M5.Lcd.setCursor(90,115);
  M5.Lcd.setTextColor(WHITE);
  M5.Lcd.setTextSize(2);
  M5.Lcd.printf("MOTIVE");

  M5.Lcd.setCursor(130,145);
  M5.Lcd.printf("TO");

  M5.Lcd.setCursor(90,175);
  M5.Lcd.printf("MOTION");
  
  delay(2000);
}

void Instructions(){
  M5.Lcd.fillScreen(BLACK);

  M5.Lcd.setCursor(20,45);
  M5.Lcd.setTextColor(WHITE);
  M5.Lcd.setTextSize(3);
  M5.Lcd.printf("Instructions");
  
  M5.Lcd.setCursor(30,100);
  M5.Lcd.setTextColor(WHITE);
  M5.Lcd.setTextSize(1);
  M5.Lcd.printf("-Click on A after entering weight");
  
  M5.Lcd.setCursor(30,125);
  M5.Lcd.printf("-Click on B after entering speed");

  M5.Lcd.setCursor(30,150);
  M5.Lcd.printf("-Click on C after stopping timer");

  M5.Lcd.setTextColor(DARKGREEN);
  M5.Lcd.setCursor(30,190);
  M5.Lcd.printf("-GOOD LUCK!");
  
  delay(10000);
}

void weightkeypad(){
  M5.Lcd.fillScreen(TFT_WHITE);
  
 M5.Lcd.fillScreen(WHITE);
 M5.Lcd.setTextColor(BLACK);
 M5.Lcd.setCursor(170,100);
 M5.Lcd.setTextSize(1);
 M5.Lcd.printf("Enter your weight\n                                  in kg,\n                                  Press A to go:");
 
     M5.Lcd.setTextColor(light);
     M5.Lcd.drawString("",110,8,4);
     M5.Lcd.setTextFont(4);
   for(int i=0;i<n2;i++)
   posX[i]=(start+(i*boxSize)+(space*i));
   M5.Lcd.fillRoundRect(posX[0],3,140,40,4,BLACK);

   for(int i=0;i<n;i++)
    for(int j=0;j<n2;j++){
        M5.Lcd.fillRoundRect(posX[i],posX[j]+22,boxSize,boxSize,4,BLACK);
        M5.Lcd.drawString(String(buttons[i][j]), posX[i]+25, posX[j]+60);
    }

    while(1){
      TouchPoint_t pos= M5.Touch.getPressPoint();
        
      if (pos.x>0) {
      if(pos.y>0 && pos.y<40)
             reset();

       if(pres==1)
       
       for(int i=0;i<n2;i++)
          for(int j=0;j<n;j++)
          {
            if(pos.x>posX[j] && pos.x<posX[j]+boxSize && pos.y>posX[i]+22 && pos.y<posX[i]+boxSize+22 ){
              M5.Lcd.fillRoundRect(posX[j],posX[i]+22,boxSize,boxSize,4,BLACK);
              if(buttons[j][i]=='0' || buttons[j][i]=='1' || buttons[j][i]=='2' || buttons[j][i]=='3' || 
                 buttons[j][i]=='4' || buttons[j][i]=='5' || buttons[j][i]=='6' || buttons[j][i]=='7' || 
                 buttons[j][i]=='8' || buttons[j][i]=='9' || buttons[j][i]=='.' || buttons[j][i]==','){
                if(math.length()<7){
                  M5.Lcd.fillRoundRect(posX[0],3,140,40,4,BLACK);
                  math=math+String(buttons[j][i]);
                }}
       
              M5.Lcd.drawString(math,160,40,4);

              pres=0;
              pos.x=0;
              pos.y=0;
      
                 M5.Lcd.setTextFont(4);
      
                 M5.Lcd.setTextColor(light);
                 M5.Lcd.fillRoundRect(posX[j],posX[i]+22,boxSize,boxSize,4,BLACK);}
             
              
              M5.Lcd.drawString(String(buttons[j][i]), posX[j]+25, posX[i]+60);       
            }
          }else{pres=1;}
       
       if(M5.BtnA.wasPressed()) {
          break;
        }
          M5.update();
      }
    M5.Lcd.fillScreen(BLACK);

}

void speedbuttons(){
  M5.Buttons.addHandler(switchColor, E_DBLTAP);
  doButtons();
tl.addHandler(touchevent1, E_DBLTAP);
bl.addHandler(touchevent2, E_DBLTAP);
tr.addHandler(touchevent3, E_DBLTAP);
br.addHandler(touchevent4, E_DBLTAP);

  while (1) {
 M5.Lcd.setTextColor(WHITE);
M5.Lcd.setTextSize(1);
M5.Lcd.setCursor(30,50);
M5.Lcd.printf("Brisk Walking");

M5.Lcd.setCursor(30,70);
M5.Lcd.printf("(3.5-4 mph)");

M5.Lcd.setCursor(200,50);
M5.Lcd.printf("Jogging");

M5.Lcd.setCursor(200,70);
M5.Lcd.printf("(5-6.5 mph)");

M5.Lcd.setCursor(30,170);
M5.Lcd.printf("Walking");

M5.Lcd.setCursor(30,190);
M5.Lcd.printf("(4.5 mph)");

M5.Lcd.setCursor(200,170);
M5.Lcd.printf("Running");

M5.Lcd.setCursor(200,190);
M5.Lcd.printf("(7 mph)");

    if(M5.BtnB.wasPressed()) {
      break;
    }
    M5.update();
        
    }
}

void stopwatch(){
   
   M5.Lcd.fillScreen(BLACK);
   M5.Lcd.setTextColor(WHITE);
  M5.Lcd.setTextSize(3);
  interrupptTimer = timerBegin(0, 80, true);
  timerAttachInterrupt(interrupptTimer, &usecTimer, true);
  timerAlarmWrite(interrupptTimer, 5, true);
  timerAlarmEnable(interrupptTimer);
  timerAlarmDisable(interrupptTimer);
  
while(1){
TouchPoint_t pos= M5.Touch.getPressPoint();

if(pos.x>0 && startCheck == 0)
{
  countStart = 1; 
}

if(pos.x>0 && startCheck == 1)
{
  countStart = 0;
}

  if (countStart == 1 && startCheck == 0)
  {
    timerAlarmEnable(interrupptTimer);
    startCheck = 1;
  }

if (countStart == 0 && startCheck == 1)
  {
    timerAlarmDisable(interrupptTimer);
    startCheck = 0;
  }

  display[2] = (int)((usecCount % 1000000) / 1000);
  display[1] = (int)((usecCount / 1000000) % 60);
  display[0] = (int)((usecCount / 60000000) % 3600);

   M5.Lcd.fillScreen(BLACK);
  M5.Lcd.setCursor(0, 106);
  M5.Lcd.printf(" m: s: ms\n");
  M5.Lcd.printf("%02d:",display[0]);
  M5.Lcd.printf("%02d:",display[1]);
  M5.Lcd.printf("%03d",display[2]);

   if(M5.BtnC.wasPressed()) {
      break;
    }
    M5.update();
}

M5.Lcd.fillScreen(BLACK);

}

void congratulations()
{
 
  M5.Lcd.fillScreen(BLACK);

  M5.Lcd.fillCircle(160,110,134,0xfd79);

  M5.Lcd.setTextSize(2);
  M5.Lcd.setCursor(30,120);
  M5.Lcd.setTextColor(WHITE);
  M5.Lcd.printf("Congratulations!");
  M5.Lcd.setCursor(34,150);
  M5.Lcd.printf("End of Workout");
  
  delay(4000);
}

void finalcalculation(){
  M5.Lcd.fillScreen(WHITE);
  M5.Lcd.fillRect(0,0,350,67,MAROON);
  M5.Lcd.setTextColor(WHITE);
  M5.Lcd.setTextSize(2);
  M5.Lcd.setCursor(7,40);
  M5.Lcd.printf("Results");
  
float finalcalc;
float exercisetime;

numbers=math.toFloat();
exercisetime = display[0]+(display[1]/60);
finalcalc = (exercisetime*numbers*MET*3.5)/200;

  M5.Lcd.setTextColor(BLACK);
  M5.Lcd.setTextSize(1.75);
  M5.Lcd.setCursor(7,100);
  M5.Lcd.printf("Calories Lost:");

  M5.Lcd.setCursor(7,120);
  M5.Lcd.printf("Total Time:");
  
  M5.Lcd.setCursor(170,100);
  M5.Lcd.print(finalcalc);

  M5.Lcd.setCursor(170,120);
  M5.Lcd.printf("%02d:",display[0]);
  M5.Lcd.printf("%02d:",display[1]);
  M5.Lcd.printf("%03d",display[2]);

  M5.Lcd.setCursor(7, 140);
  M5.Lcd.printf("MET Value:");

  M5.Lcd.setCursor(170, 140);
  M5.Lcd.print(MET);

  M5.Lcd.setCursor(7, 160);
  M5.Lcd.printf("Weight:");

  M5.Lcd.setCursor(170, 160);
  M5.Lcd.print(numbers);
  
  M5.Lcd.drawLine(5,105,260,105, DARKGREEN);
  M5.Lcd.drawLine(5,125,260,125, DARKGREEN);
  M5.Lcd.drawLine(5,145,260,145, DARKGREEN);
  
 delay(7200000);
}

void setup() {
M5.begin();
screenstate = 1;

}

void loop() {
  M5.update();

  switch(screenstate) {
    case 1:
    {
      screenstate=2;
      Introduction();
      break;
    }

    case 2:
    {
      screenstate=3;
      Instructions();
      break;
    }

    case 3:
    {
      screenstate=4;
      weightkeypad();
      break;
    }

    case 4: 
    {
      screenstate=5;
      speedbuttons();
      break;
    }

    case 5:
    {
      screenstate=6;
      stopwatch();
      break;
    }

    case 6:
    {
      congratulations();
      finalcalculation();
      break;
    }
  }
   

}
