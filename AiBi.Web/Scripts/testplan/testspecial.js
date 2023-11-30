//#region 全局对象
var global = {
    localId: 1,
    localPage: null,
    localExampleIndex: null,
    localExampleType: null,
    localQuestionIndex: null,
    timer: null,
    BalanceSeconds: 0,
    setTimer: function (count) {
        global.BalanceSeconds = count
    }
};
//#endregion

layui.config({
    base: "/js/"
}).use(['vue'], async function () {

    //#region 单页类
    class SinglePage {
        funcs = {};
        
        constructor(id, data) {
            this.id = id + "_" + (global.localId++);
            this.oid = id;
            this.el = null;
            this.data = data;
            this.isInit = false;
            
        }
        init() {
            if (this.isInit) {
                return;
            }
            this.el = $("#" + this.oid).clone().attr("id", this.id).show().appendTo("body")[0];
            if (typeof this.beforeLoad == "function") {
                this.beforeLoad();
            }
            this.data = $.extend(this.data, this.funcs);
            this.vue = new Vue({
                el: "#" + this.id,
                data: this.data
            });
            this.isInit = true;
            if (typeof this.onload == "function") {
                this.onload();
            }
            return this;
        }
        close() {
            this.isClose = true;
            delete this.vue;
            $("#" + this.id).remove();
        }
    }
    //#endregion

    //#region 首页
    class MainPage extends SinglePage {
        funcs = {
            toNext: function (e) {
                toExampleStart(0);
            }
        };
        onload() {
            var that = this;
            setTimeout(function () {
                if (that.isClose) return;
                
                that.funcs.toNext();
            },3000);
        }
    }
    //#endregion

    //#region 第一题

    //#region 第一题第一页
    class FirstPage1 extends SinglePage {
        onSpaceDown(e) {
            toPage(new DescPage1("desc_1", {}));
        }
    }
    //#endregion

    //#region 第一题说明页
    class DescPage1 extends SinglePage {

        async onSpaceDown(e) {
            var example = plan.BusTestPlanExamples[global.localExampleIndex].Example;
            var url = "/Res/1/sybsrhprac.txt";
            if (example.Title.indexOf("乙") > 0) {
                url = "/Res/1/sybsrhprac2.txt";
            }
            var testtxt = await (await fetch(url)).text();
            var lines = testtxt.split("\r\n").filter((a, i) => i > 0 && a != "").map(b => b.split(/\s/).map(b => parseInt(b)));
            TestPage1.setAnswer();
            var obj = TestPage1.getImgData(0, lines);
            obj.istest = true;
            toPage(new TestPage1("question_1", obj));
        }
    }
    //#endregion

    //#region 第一题练习页
    class TestPage1 extends SinglePage {
        static getImgUrl(a, b) {
            return '/Res/1/' + (a.toString(16) + b.toString(16)) + '.png';
        }
        static getImgData(index, lines) {
            let line = lines[index];
            return {
                index: index,
                line: line,
                lines: lines,
                question: {
                    t0url: TestPage1.getImgUrl(line[0], line[1]),
                    t1url: TestPage1.getImgUrl(line[3], line[4]),
                    t2url: TestPage1.getImgUrl(line[6], line[7]),
                    t3url: TestPage1.getImgUrl(line[9], line[10]),
                    t4url: TestPage1.getImgUrl(line[12], line[13]),
                    t5url: TestPage1.getImgUrl(line[15], line[16]),
                    t6url: TestPage1.getImgUrl(line[18], line[19]),

                    exist: line[21],
                },
                BalanceSeconds: global.BalanceSeconds
            };
        }
        static answerData = null;
        static setAnswer(obj) {
            if (!obj) {
                TestPage1.answerData = {
                    score: 0,
                    list: []
                };
                return;
            }
            TestPage1.answerData.list.push(obj);
            TestPage1.answerData.score += obj.score;
        }
        isanswer = false;
        isover = false;
        answer(exist) {
            if (this.isanswer) return;
            this.isanswer = true;
            if (this.data.question.exist == exist) {
                TestPage1.setAnswer({ score: 1, result: exist });
                this.toNext();
            } else {
                TestPage1.setAnswer({ score: 0, result: exist });
                $(this.el).find(".msgDiv").show();
                var that = this;
                setTimeout(function () {
                    if (that.isover) {
                        return;
                    }
                    that.toNext();
                }, 750);
            }
        }

        toNext() {
            if (this.data.index + 1 >= this.data.lines.length) {
                this.finishTest();
                return;
            }
            var obj = TestPage1.getImgData(this.data.index + 1, this.data.lines);
            obj.istest = this.data.istest;
            toPage(new TestPage1("question_1", obj));
        }
        finishTest() {
            var rate = TestPage1.answerData.list.sum((i, a) => (a.score || 0) > 0 ? 1 : 0) / TestPage1.answerData.list.length;
            if (this.data.istest) {
                toPage(new TestEndPage1("testEnd_1", { lines: this.data.lines, succrate: (Math.round(rate * 10000) / 100).toFixed(2) }));
                return;
            }
            this.finishQuestion();
        }

        onJDown(e) {
            this.answer(1);
        }
        onFDown(e) {
            this.answer(0);
        }

        ontimer(count) {
            if (this.data.istest) return;
            $(this.el).find(".timerfont").html("剩余时间:" + count + "秒");
            if (count <= 0) {
                this.finishTest();
            }
        }
        finishQuestion() {
            this.isover = true;
            toPage(new FinishOverPage("finishOver", {}));
        }
    }
    //#endregion

    //#region 第一题测试结束页
    class TestEndPage1 extends SinglePage {
        funcs = {
            onprev: function (e) {
                TestPage1.setAnswer();
                var obj = TestPage1.getImgData(0, TestEndPage1.lines);
                obj.istest = true;
                toPage(new TestPage1("question_1", obj));
            },
            onnext: function () {
                toPage(new AnswerStartPage1("answerStart_1", {}));
            }
        };
        onload() {
            TestEndPage1.lines = this.data.lines;
        }
    }
    //#endregion

    //#region 测试开始键盘提示1
    class AnswerStartPage1 extends SinglePage {
        async onSpaceDown() {
            var example = plan.BusTestPlanExamples[global.localExampleIndex].Example;
            var url = "/Res/1/test.txt";
            if (example.Title.indexOf("乙") > 0) {
                url = "/Res/1/test2.txt";
            }
            var testtxt = await(await fetch(url)).text();
            var lines = testtxt.split("\r\n").filter((a, i) => i > 0 && a != "").map(b => b.split(/\s/).map(b => parseInt(b)));
            TestPage1.setAnswer();
            global.setTimer(120);
            toPage(new TestPage1("question_1", TestPage1.getImgData(0, lines)));
        }
    }
    //#endregion

    //#endregion

    //#region 第二题
    let NumberExample = {
        "1":"00",
        "2":"10",
        "3":"21",
        "4":"01",
        "5":"12",
        "6":"02",
        "7":"11",
        "8":"22",
        "9":"20",
    };
    //#region 第二题第一页
    class FirstPage2 extends SinglePage {
        onSpaceDown(e) {
            toPage(new TestPage2("test_2", {}));
        }
    }
    //#endregion

    //#region 第二题练习
    class TestPage2 extends SinglePage {
        answer = ["9", "6", "1", "3", "7", "8", "5", "3"].map((a, i) => NumberExample[a]).join("");
        localIndex = 0;
        myAnswer = [];
        canAnswer = true;
        imgs = {
            "0":'<img src="/Res/2/space.png" />',
            "1":'<img src="/Res/2/X.png" />',
            "2":'<img src="/Res/2/O.png" />',
        };

        renderPos() {
            var p = $(this.el).find(".idPointer");
            var txt = $(this.el).find(".idPointerTxt");
            var s1 = $(this.el).find(".s" + Math.min(this.localIndex + 1, 16));
            txt.css("marginLeft", s1.position().left + (s1.width() / 2 - 10) + "px");
            p.css("marginLeft", s1.position().left + (s1.width() / 2 - 10) + "px");
        }
        onload() {
            this.renderPos();
        }
        toAnswer(c) {
            if (!this.canAnswer || this.localIndex>=16) {
                return;
            }
            $(this.el).find(".s" + (this.localIndex+1)).html(this.imgs[c]);
            if (this.answer[this.localIndex] == c) {
                this.myAnswer.push({ score: 1, result :1});
            } else {
                this.myAnswer.push({ score: 0, result: 0 });
                this.canAnswer = false;
                $(this.el).find(".errDiv p font").css("visibility", "visible");
                var that = this;
                setTimeout(function () {
                    that.canAnswer = true;
                    $(that.el).find(".errDiv p font").css("visibility", "hidden");
                }, 750);
            }
            this.localIndex++;
            $(this.el).find(".idPointerTxt").hide();
            this.renderPos();

            if (this.localIndex >= 16) {
                var data = {
                    succrate : (Math.round(this.myAnswer.sum((i, a) => a.result) / this.myAnswer.length * 10000) / 100).toFixed(2)
                };
                setTimeout(function () {
                    toPage(new TestEndPage2("testEnd_2", data));
                }, 750);
                
            }
        }
        onresize() {
            this.renderPos();
        }
        onSpaceDown() {
            this.toAnswer('0');
        }
        onFDown() {
            this.toAnswer('1');
        }
        onJDown() {
            this.toAnswer('2');
        }

    }
    //#endregion

    //#region 第二题测试结束页
    class TestEndPage2 extends SinglePage {
        funcs = {
            onprev: function (e) {
                toPage(new TestPage2("test_2", {}));
            },
            onnext: function () {
                toPage(new AnswerStartPage2("answerStart_2", {}));
            }
        };
        onload() {
            TestEndPage1.lines = this.data.lines;
        }
    }
    //#endregion

    //#region 测试开始键盘提示1
    class AnswerStartPage2 extends SinglePage {
        async onSpaceDown() {
            var example = plan.BusTestPlanExamples[global.localExampleIndex].Example;
            var url = "/Res/2/test.txt";
            if (example.Title.indexOf("乙") > 0) {
                url = "/Res/2/test2.txt";
            }
            var testtxt = await (await fetch(url)).text();
            var lines = testtxt.split("\r\n").filter(a => a);

            global.setTimer(120);
            toPage(new QuestionPage2("question_2", QuestionPage2.getData(0, lines)));
        }
    }
    //#endregion

    //#region 第二题考题2
    class QuestionPage2 extends SinglePage {
        localIndex = 0;
        myAnswer = [];
        canAnswer = true;
        imgs = {
            "0": '<img src="/Res/2/space.png" />',
            "1": '<img src="/Res/2/X.png" />',
            "2": '<img src="/Res/2/O.png" />',
        };
        static getData(index, lines) {
            let line = lines[index*3] + lines[index*3 + 1] + lines[index*3+2];
            return {
                index: index,
                line: line,
                lines: lines,
                answer: line.split("").map((b, i) => NumberExample[b]).join(""),
                BalanceSeconds: global.BalanceSeconds
            };
        }
        renderPos() {
            var p3 = $(this.el).find(".idPointer0").css("visibility", "hidden");
            var p1 = $(this.el).find(".idPointer1").css("visibility","hidden");
            var p2 = $(this.el).find(".idPointer2").css("visibility", "hidden");

            var p = $(this.el).find(".idPointer" + Math.min(parseInt(this.localIndex / 20),2)).css("visibility", "visible");
            var s1 = $(this.el).find(".s" + Math.min(this.localIndex, 59));
            p.css("marginLeft", s1.position().left + (s1.width() / 2 - 10) + "px");
        }
        onresize() {
            this.renderPos();
        }
        onload() {
            $(this.el).find("td[colspan=2]").filter(i => i >= 9).each((i, a) => $(a).html(this.data.line[i]));
            this.renderPos();
            this.answer = this.data.answer;
        }
        ontimer(second) {
            $(this.el).find(".timerDiv p font").html('剩余时间:' + second + '秒');
            if (second <= 0) {
                this.finishQuestion();
            }
        }
        onSpaceDown() {
            this.toAnswer('0');
        }
        onFDown() {
            this.toAnswer('1');
        }
        onJDown() {
            this.toAnswer('2');
        }
        toAnswer(c) {
            if (!this.canAnswer || this.localIndex >= this.answer.length) {
                return;
            }
            $(this.el).find(".s" + (this.localIndex)).html(this.imgs[c]);
            if (this.answer[this.localIndex] == c) {
                this.myAnswer.push({ score: 1, result: 1 });
            } else {
                this.myAnswer.push({ score: 0, result: 0 });
                this.canAnswer = false;
                $(this.el).find(".errDiv").css("visibility", "visible");
                var that = this;
                setTimeout(function () {
                    that.canAnswer = true;
                    $(that.el).find(".errDiv").css("visibility", "hidden");
                }, 750);
            }
            this.localIndex++;
            this.renderPos();

            if (this.localIndex >= this.answer.length) {
                var that = this;
                setTimeout(function () {
                    that.toNext();
                }, 750);
            }
        }
        toNext() {
            if (this.data.index*3 + 3 >= this.data.lines.length) {
                this.finishQuestion();
                return;
            }
            var obj = QuestionPage2.getData(this.data.index + 1, this.data.lines);
            toPage(new QuestionPage2("question_2", obj));

        }
        finishQuestion() {
            toPage(new FinishOverPage("finishOver", {}));
        }
    }
    //#endregion

    //#endregion

    //#region 第11题

    //#region 第11题第一页
    class FirstPage11 extends SinglePage {
        funcs = {
            start: async function () {
                var example = plan.BusTestPlanExamples[global.localExampleIndex].Example;
                var url = "/Res/11/chars.txt";
                if (example.Title.indexOf("乙") > 0) {
                    url = "/Res/11/chars2.txt";
                }
                var testtxt = await (await fetch(url)).text();
                var lines = testtxt.split("\r\n").filter((a, i) => i > 0 && a).map(a => a.split(/\s+/)).reduce((s,a,i) => {
                    var last = s.pop();
                    if (last && last.words.length == last.span) {
                        s.push(last);
                        last = null;
                    }
                    if (!last ) {
                        s.push({weight:a[0],span:a[1],word:a[2],words:[a[2]]});
                        return s;
                    }
                    last.words.push(a[2]);
                    s.push(last);
                    return s;
                }, []);
                //console.log(lines);
                toPage(new ShowAnimalOnlyTitlePage11("greenword_11", { title: "记属相", lines: lines,index:0 }));
            }
        }
    }
    //#endregion

    //#region 第11题显示记属相
    class ShowAnimalOnlyTitlePage11 extends SinglePage {
        onload() {
            var data = $.extend({},this.data);
            setTimeout(function () {
                data.wordIndex = 0;
                data.title = data.lines[data.index].word;
                toPage(new ShowAnimalOnlyPage11("animalsShow_11", data));
            }, 1000);
        }
    }
    //#endregion

    //#region 第11题限时属相
    class ShowAnimalOnlyPage11 extends SinglePage {
        onload() {
            var data = $.extend({}, this.data);
            data.wordIndex++;
            setTimeout(function () {
                let line = data.lines[data.index];
                if (data.wordIndex >= line.span) {
                    toPage(new ShowAnimalOnlyAnswerPage11("animalsAnswer_11", data));
                } else {
                    data.title = line.words[data.wordIndex];
                    toPage(new ShowAnimalOnlyPage11("animalsShow_11", data));
                }
            }, 1000);
        }
    }
    //#endregion

    //#region 第11题回答记属相
    class ShowAnimalOnlyAnswerPage11 extends SinglePage {
        myAnswer = [];
        funcs = {
            setvalue: function (e) {
                var inputIndex = global.localPage.myAnswer.length;
                var ipt = $(global.localPage.el).find('.span_que').eq(inputIndex);
                if (ipt.length <= 0) {
                    return;
                }
                var word = $(e.target).attr("que");
                $(e.target).css("borderColor","orange");
                global.localPage.myAnswer.push(word);
                $(global.localPage.el).find('button').prop("disabled", false);
                ipt.val(word);
            },
            clearBox: function (e) {
                var inputIndex = global.localPage.myAnswer.length - 1;
                var ipt = $(global.localPage.el).find('.span_que').eq(inputIndex);
                if (ipt.length <= 0) {
                    return;
                }
                var word = global.localPage.myAnswer.pop();
                $(global.localPage.el).find('[que=' + word + ']').css("borderColor", "white");
                ipt.val("");
                if (inputIndex <= 0) {
                    $(global.localPage.el).find('button').prop("disabled", true);
                }
            },
            checkBox: function (e) {
                let page = global.localPage;
                let data = page.data;
                let line = data.lines[data.index];
                let res = line.words.join('');
                let myRes = page.myAnswer.join("");
                let span = line.span;
                let nextIndex;
                if (res == myRes) {
                    nextIndex = data.lines.findIndex((a, i) => a.span > span);
                } else {
                    nextIndex = data.lines.findIndex((a, i) => i > data.index || a.span > span);
                }
                if (nextIndex >= 0) {
                    data.title = "记属相";
                    data.index = nextIndex;
                    toPage(new ShowAnimalOnlyTitlePage11("greenword_11", data));
                    return;
                }
                //跳
                toPage(new HeartTipPage11("heartTip_11", {}));
            }
        };
        onload() {
            this.line = this.data.lines[this.data.index];
            var span = parseInt(this.line.span);
            var span_0 = $(this.el).find('.span_0');
            Array(span-1).fill(0).forEach(a => {
                span_0.after(span_0.clone());
            });
        }
    }
    //#endregion

    //#region 第11题心酸提示
    class HeartTipPage11 extends SinglePage {
        funcs = {
            start: async function () {
                var example = plan.BusTestPlanExamples[global.localExampleIndex].Example;
                var url = "/Res/11/opspanbaseline.txt";
                if (example.Title.indexOf("乙") > 0) {
                    url = "/Res/11/opspanbaseline2.txt";
                }
                var testtxt = await (await fetch(url)).text();
                var lines = testtxt.split("\r\n").filter((a, i) => i > 0 && a).map(a => a.split(/\s+/));
                //console.log(lines);
                toPage(new ShowHeartTitlePage11("greenword_11", { title: "做心算", lines: lines, index: 0 }));
            }
        }
    }
    //#endregion

    //#region 第11题显示做心算
    class ShowHeartTitlePage11 extends SinglePage {
        onload() {
            var data = $.extend({}, this.data);
            setTimeout(function () {
                toPage(new HeartTestPage11("heartTest_11", data));
            }, 1000);
        }
    }
    //#endregion

    //#region 第11题做心算练习
    class HeartTestPage11 extends SinglePage {
        funcs = {
            callRight: function (e) {
                global.localPage.answer(1);
            },
            callWrong: function (e) {
                global.localPage.answer(2);
            }
        }
        isanswer = false;
        timeout = false;
        onload() {
            var line = this.data.lines[this.data.index];
            $(this.el).find(".timian").html(line[1]+line[2]);
            var that = this;
            setTimeout(function () {
                that.timeout = true;
                $(that.el).find(".timerDiv").css("visibility","visible");
            }, 8000);
        }

        answer(ret) {
            if (this.isanswer) { return; }
            this.isanswer = true;
            var line = this.data.lines[this.data.index];
            var res = line[3];
            if (ret == res) {
                this.next();
                return;
            }
            $(this.el).find(".timerDiv").css("visibility", "visible").find("font").html("错误");
            var that = this;
            setTimeout(function () {
                that.next();
            }, 750);
        }
        next() {
            var data = $.extend({}, this.data);
            data.index++;
            if (data.index < data.lines.length) {
                toPage(new HeartTestPage11("heartTest_11", data));
                return;
            }
            toPage(new BothTipPage11("bothTestTip_11", {}));
        }
    }
    //#endregion

    //#region 第11题同时提示
    class BothTipPage11 extends SinglePage {
        funcs = {
            start: async function () {
                var example = plan.BusTestPlanExamples[global.localExampleIndex].Example;
                var url = "/Res/11/opspan.txt";
                if (example.Title.indexOf("乙") > 0) {
                    url = "/Res/11/opspan2.txt";
                }
                var testtxt = await (await fetch(url)).text();
                var lines = testtxt.split("\r\n").filter((a, i) => i > 0 && a).map(a => a.split(/\s+/)).reduce((s, a, i) => {
                    var last = s.pop();
                    if (last && last.words.length == last.span) {
                        s.push(last);
                        last = null;
                    }
                    if (!last) {
                        s.push({ weight: a[0], span: a[7], word: a[4], words: [a[4]], tis: [{ timian: a[1] + a[2], ret: a[3] }], lines: [a] });
                        return s;
                    }
                    last.words.push(a[4]);
                    last.tis.push({ timian: a[1] + a[2], ret: a[3] });
                    last.lines.push(a);
                    s.push(last);
                    return s;
                }, []);
                var test = lines.shift();
                //console.log(lines);
                toPage(new ShowBothTitlePage11("bothTestTitle_11", { span: test.span,spanid:1, lines: lines, test: test, index: 0 }));
            }
        }
    }
    //#endregion

    class BothQuestionPage11 extends SinglePage {
        getLine() {
            var line = this.data.lines[this.data.index];
            if (this.data.test) {
                line = this.data.test;
            }
            return line;
        }
    }

    //#region 第11题显示全做
    class ShowBothTitlePage11 extends BothQuestionPage11 {
        onload() {
            let data = $.extend({},this.data);
            setTimeout(function () {
                data.index2 = 0;
                toPage(new HeartQuestionPage11("heartTest_11", data));
            }, 1000);
        }
    }
    //#endregion

    //#region 第11题全部做心算
    class HeartQuestionPage11 extends BothQuestionPage11 {
        funcs = {
            callRight: function (e) {
                global.localPage.answer(1);
            },
            callWrong: function (e) {
                global.localPage.answer(2);
            }
        }
        isanswer = false;
        timeout = false;
        onload() {
            var line = this.getLine();
            var index2 = this.data.index2;
            $(this.el).find(".timian").html(line.tis[index2].timian);
            var that = this;
            setTimeout(function () {
                that.timeout = true;
                $(that.el).find(".timerDiv").css("visibility", "visible");
            }, 8000);
        }
        
        answer(ret) {
            if (this.isanswer) { return; }
            this.isanswer = true;
            var line = this.getLine();
            var index2 = this.data.index2;
            var res = line.tis[index2].ret;
            if (ret == res) {
                this.saveAnswer(true, ret);
                this.next();
                return;
            }
            this.saveAnswer(false, ret);
            $(this.el).find(".timerDiv").css("visibility", "visible").find("font").html("错误");
            var that = this;
            setTimeout(function () {
                that.next();
            }, 750);
        }
        next() {
            var data = $.extend({}, this.data);
            var line = this.getLine();
            var index2 = this.data.index2;
            data.title = line.words[index2];
            toPage(new ShowAnimalPage11("animalsShow_11", data));
        }
        saveAnswer(isright, answer) {
            var line = this.getLine();
            var index2 = this.data.index2;
            this.data.myAnswer = this.data.myAnswer || [];
            this.data.myAnswer.push({ status: this.timeout ? 0 : (isright ? 1 : -1), isright: isright, ti: line.tis[index2].timian, answer: answer });
        }
    }
    //#endregion

    //#region 第11题正式显示属相
    class ShowAnimalPage11 extends BothQuestionPage11 {
        onload() {
            var data = $.extend({}, this.data);
            data.index2++;
            var line = this.getLine();
            setTimeout(function () {
                if (data.index2 < line.words.length) {
                    toPage(new HeartQuestionPage11("heartTest_11", data));
                    return;
                }
                global.setTimer(45);
                data.showtimer = true;
                data.BalanceSeconds = 45;
                toPage(new ShowAnimalAnswerPage11("animalsAnswer_11",data));

            }, 1000);
        }
    }
    //#endregion


    //#region 第11题回答记属相
    class ShowAnimalAnswerPage11 extends BothQuestionPage11 {
        timeout = false;
        myAnswer = [];
        funcs = {
            setvalue: function (e) {
                var inputIndex = global.localPage.myAnswer.length;
                var ipt = $(global.localPage.el).find('.span_que').eq(inputIndex);
                if (ipt.length <= 0) {
                    return;
                }
                var word = $(e.target).attr("que");
                $(e.target).css("borderColor", "orange");
                global.localPage.myAnswer.push(word);
                $(global.localPage.el).find('button').prop("disabled", false);
                ipt.val(word);
            },
            clearBox: function (e) {
                var inputIndex = global.localPage.myAnswer.length - 1;
                var ipt = $(global.localPage.el).find('.span_que').eq(inputIndex);
                if (ipt.length <= 0) {
                    return;
                }
                var word = global.localPage.myAnswer.pop();
                $(global.localPage.el).find('[que=' + word + ']').css("borderColor", "white");
                ipt.val("");
                if (inputIndex <= 0) {
                    $(global.localPage.el).find('button').prop("disabled", true);
                }
            },
            checkBox: function (e) {
                let page = global.localPage;
                let data = page.data;
                let line = page.getLine();
                let res = line.words.join('');
                let myRes = page.myAnswer.join("");
                let span = line.span;
                if (res == myRes) {
                    page.saveAnswer(true, myRes);
                } else {
                    page.saveAnswer(false, myRes);
                }
                if (data.test) {
                    page.processTest();
                    return;
                }
                page.processQuestion();
            }
        };
        onload() {
            this.line = this.getLine();
            var span = parseInt(this.line.span);
            var span_0 = $(this.el).find('.span_0');
            Array(span - 1).fill(0).forEach(a => {
                span_0.after(span_0.clone());
            });
        }
        ontimer(second) {
            $(this.el).find(".timerDiv_lb p font").html('剩余时间:' + second + '秒');
            if (second <= 0) {
                this.timeout = true;
                $(this.el).find(".timerDiv_lb p font").html('超时');
            }
        }
        saveAnswer(isright, answer) {
            var line = this.getLine();
            this.data.myAnswer = this.data.myAnswer || [];
            this.data.myAnswer.push({ status: this.timeout ? 0 : (isright ? 1 : -1), isright: isright, ti: line.words.join(""), answer: answer });
        }
        getMyAnswer() {
            return this.data.myAnswer.sum((i, a) => a.status == 1 ? 1 : 0) / this.data.myAnswer.length;
        }
        processTest() {
            var score = this.getMyAnswer();
            if (score >= 0.9 || this.data.istest2) {
                this.data.myAnswer = null;
                this.data.test = null;
                toPage(new BothQuestionTip11("bothQuestionTip_11", this.data));
                return;
            }
            this.data.istest2 = true;
            this.data.myAnswer = null;
            toPage(new BothTestRetry11("bothTestRetry_11", this.data));
        }
        processQuestion() {
            var bili = this.getMyAnswer();
            var index = this.data.index;
            var line = this.getLine();
            var nextIndex
            var score = this.getMyAnswer();
            if (score >= 0.9) {
                nextIndex = this.data.lines.findIndex((a, i) => {
                    return i > index && a.span > line.span;
                });
            } else {
                nextIndex = this.data.lines.findIndex((a, i) => {
                    return i > index && a.span == line.span;
                });
            }
            if (nextIndex < 0) {
                toPage(new FinishOverPage("finishOver", {}));
                return;
            }
            if (score < 0.9) {
                this.data.spanid++;
            }
            this.data.span = this.data.lines[nextIndex].span;
            this.data.myAnswer = null;
            this.data.index = nextIndex;
            toPage(new ShowBothTitlePage11("bothTestTitle_11", this.data));
        }
    }
    //#endregion

    //#region 第11题正式显示属相
    class BothTestRetry11 extends SinglePage {
        onload() {
            var data = $.extend({}, this.data);
            setTimeout(function () {
                toPage(new ShowBothTitlePage11("bothTestTitle_11", data));
            }, 1000);
        }
    }
    //#endregion

    //#region 第11题正式显示属相
    class BothQuestionTip11 extends SinglePage {
        funcs = {
            start: function () {
                let page = global.localPage;
                var data = $.extend({}, page.data);
                toPage(new ShowBothTitlePage11("bothTestTitle_11", data));
            }
        };
    }
    //#endregion

    //#endregion

    //#region 第12题

    //#region 第12题第一页
    class FirstPage12 extends SinglePage {
        funcs = {
            start: async function () {
                var example = plan.BusTestPlanExamples[global.localExampleIndex].Example;
                var url = "/Res/12/locationExe.txt";
                //if (example.Title.indexOf("乙") > 0) {
                //    url = "/Res/12/chars2.txt";
                //}
                var testtxt = await (await fetch(url)).text();
                var lines = testtxt.split("\r\n").filter((a, i) => i > 0 && a).map(a => a.split(/\s+/)).reduce((s, a, i) => {
                    var last = s.pop();
                    if (last && last.locs.length == last.span) {
                        s.push(last);
                        last = null;
                    }
                    if (!last) {
                        s.push({ weight: a[0], span: a[1], loc: a[2], locs: [a[2]] });
                        return s;
                    }
                    last.locs.push(a[2]);
                    s.push(last);
                    return s;
                }, []);
                //console.log(lines);
                toPage(new ShowLocOnlyTitlePage12("greenword_11", { title: "记位置", lines: lines, index: 0 }));
            }
        }
    }
    //#endregion

    //#region 第12题显示记位置
    class ShowLocOnlyTitlePage12 extends SinglePage {
        onload() {
            var data = $.extend({}, this.data);
            setTimeout(function () {
                data.locIndex = 0;
                data.loc = data.lines[data.index].loc;
                toPage(new ShowLocOnlyPage12("locshow_12", data));
            }, 1000);
        }
    }
    //#endregion

    //#region 第12题显示位置
    class ShowLocOnlyPage12 extends SinglePage {
        onload() {
            var data = $.extend({}, this.data);
            data.locIndex++;
            setTimeout(function () {
                let line = data.lines[data.index];
                if (data.locIndex >= line.span) {
                    toPage(new ShowLocOnlyAnswerPage12("locAnswer_12", data));
                } else {
                    data.loc = line.locs[data.locIndex];
                    toPage(new ShowLocOnlyPage12("locshow_12", data));
                }
            }, 1000);
        }
    }
    //#endregion

    //#region 第12题回答记位置
    class ShowLocOnlyAnswerPage12 extends SinglePage {
        myAnswer = [];
        funcs = {
            setvalue: function (e) {
                var page = global.localPage;
                $(page.el).find(".btnTop button").prop("disabled", false);
                var data = page.data;
                var line = data.lines[data.index];
                if (page.myAnswer.length >= line.span) {
                    return;
                }
                var ballNo = page.myAnswer.length + 1;
                var url = "/Res/12/redgood" + ballNo + ".png";
                var parent = $(e.target).closest("td");
                $(e.target).hide();
                parent.append('<img src="' + url + '">');
                page.myAnswer.push(parent.attr("tid").replace("td_",""));
            },
            clearBox: function (e) {
                var page = global.localPage;
                if (page.myAnswer.length <= 0) {
                    
                    return;
                }
                var locNo = page.myAnswer.pop();
                var td = $(page.el).find("td[tid=td_" + locNo + "]");
                td.find("img").remove();
                td.find("input").show();
                if (page.myAnswer.length <= 0) {
                    $(page.el).find(".btnTop button").prop("disabled", true);
                }
            },
            checkBox: function (e) {
                let page = global.localPage;
                let data = page.data;
                let line = data.lines[data.index];
                let res = line.locs.join('>');
                let myRes = page.myAnswer.join(">");
                let span = line.span;
                let nextIndex;
                if (res == myRes) {
                    nextIndex = data.lines.findIndex((a, i) => a.span > span);
                } else {
                    nextIndex = data.lines.findIndex((a, i) => i > data.index || a.span > span);
                }
                if (nextIndex >= 0) {
                    data.title = "记位置";
                    data.index = nextIndex;
                    toPage(new ShowLocOnlyTitlePage12("greenword_11", data));
                    return;
                }
                //跳
                toPage(new SymmetryTipPage12("symmetryTip_12", {}));
            }
        };
        onload() {
            
        }
    }
    //#endregion

    //#region 第12题对称提示
    class SymmetryTipPage12 extends SinglePage {
        funcs = {
            start: function () {
                
                toPage(new SymmetryTip2Page12("symmetryTip2_12", {}));
            }
        }
    }
    //#endregion

    //#region 第12题对称提示2
    class SymmetryTip2Page12 extends SinglePage {
        funcs = {
            start: async function () {
                var example = plan.BusTestPlanExamples[global.localExampleIndex].Example;
                var url = "/Res/12/fixedSymmExeBaseline.txt";
                //if (example.Title.indexOf("乙") > 0) {
                //    url = "/Res/11/opspanbaseline2.txt";
                //}
                var testtxt = await (await fetch(url)).text();
                var lines = testtxt.split("\r\n").filter((a, i) => i > 0 && a).map(a => a.split(/\s+/));
                //console.log(lines);
                toPage(new ShowSymmetryTitlePage12("greenword_11", { title: "判对称", lines: lines, index: 0 }));
            },
            back: function () {
                toPage(new SymmetryTipPage12("symmetryTip_12", {}));
            }
        }
    }
    //#endregion

    //#region 第12题显示做对称
    class ShowSymmetryTitlePage12 extends SinglePage {
        onload() {
            var data = $.extend({}, this.data);
            setTimeout(function () {
                toPage(new SymmetryTestPage12("symmetryTest_12", data));
            }, 1000);
        }
    }
    //#endregion

    //#region 第12题做对称练习
    class SymmetryTestPage12 extends SinglePage {
        funcs = {
            callRight: function (e) {
                global.localPage.answer(1);
            },
            callWrong: function (e) {
                global.localPage.answer(2);
            }
        }
        isanswer = false;
        timeout = false;
        onload() {
            var line = this.data.lines[this.data.index];
            $(this.el).find(".tutu").attr("src", '/Res/12/' + line[1]+'.png');
            var that = this;
            setTimeout(function () {
                that.timeout = true;
                $(that.el).find(".timerDiv").css("visibility", "visible");
            }, 5000);
        }

        answer(ret) {
            if (this.isanswer) { return; }
            this.isanswer = true;
            var line = this.data.lines[this.data.index];
            var res = line[3];
            if (ret == res) {
                this.next();
                return;
            }
            $(this.el).find(".timerDiv").css("visibility", "visible").find("font").html("错误");
            var that = this;
            setTimeout(function () {
                that.next();
            }, 750);
        }
        next() {
            var data = $.extend({}, this.data);
            data.index++;
            if (data.index < data.lines.length) {
                toPage(new SymmetryTestPage12("symmetryTest_12", data));
                return;
            }
            toPage(new BothTipPage12("bothTestTip_12", {}));
        }
    }
    //#endregion

    //#region 第12题同时提示
    class BothTipPage12 extends SinglePage {
        funcs = {
            start: async function () {
                var example = plan.BusTestPlanExamples[global.localExampleIndex].Example;
                var url = "/Res/12/fixedItems.txt";
                if (example.Title.indexOf("乙") > 0) {
                    url = "/Res/12/fixedItems2.txt";
                }
                var testtxt = await (await fetch(url)).text();
                var lines = testtxt.split("\r\n").filter((a, i) => i > 0 && a).map(a => a.split(/\s+/)).reduce((s, a, i) => {
                    var last = s.pop();
                    if (last && last.locs.length == last.span) {
                        s.push(last);
                        last = null;
                    }
                    if (!last) {
                        s.push({ weight: a[0], span: a[7], loc: a[4], locs: [a[4]], tis: [{ tutu: a[1], ret: a[3] }], lines: [a] });
                        return s;
                    }
                    last.locs.push(a[4]);
                    last.tis.push({ tutu: a[1], ret: a[3] });
                    last.lines.push(a);
                    s.push(last);
                    return s;
                }, []);
                var test = lines.shift();
                //console.log(lines);
                toPage(new ShowBothTitlePage12("bothTestTitle_12", { span: test.span, spanid: 1, lines: lines, test: test, index: 0 }));
            }
        }
    }
    //#endregion

    //#region 第12题显示全做
    class ShowBothTitlePage12 extends BothQuestionPage11 {
        onload() {
            let data = $.extend({}, this.data);
            setTimeout(function () {
                data.index2 = 0;
                toPage(new SymmetryQuestionPage12("symmetryTest_12", data));
            }, 1000);
        }
    }
    //#endregion

    //#region 第12题全部做对称
    class SymmetryQuestionPage12 extends BothQuestionPage11 {
        funcs = {
            callRight: function (e) {
                global.localPage.answer(1);
            },
            callWrong: function (e) {
                global.localPage.answer(2);
            }
        }
        isanswer = false;
        timeout = false;
        onload() {
            var line = this.getLine();
            var index2 = this.data.index2;
            $(this.el).find(".tutu").attr("src", '/Res/12/' + line.tis[index2].tutu + '.png');
            var that = this;
            setTimeout(function () {
                that.timeout = true;
                $(that.el).find(".timerDiv").css("visibility", "visible");
            }, 5000);
        }

        answer(ret) {
            if (this.isanswer) { return; }
            this.isanswer = true;
            var line = this.getLine();
            var index2 = this.data.index2;
            var res = line.tis[index2].ret;
            if (ret == res) {
                this.saveAnswer(true, ret);
                this.next();
                return;
            }
            this.saveAnswer(false, ret);
            $(this.el).find(".timerDiv").css("visibility", "visible").find("font").html("错误");
            var that = this;
            setTimeout(function () {
                that.next();
            }, 750);
        }
        next() {
            var data = $.extend({}, this.data);
            var line = this.getLine();
            var index2 = this.data.index2;
            data.loc = line.locs[index2];
            toPage(new ShowLocPage12("locshow_12", data));
        }
        saveAnswer(isright, answer) {
            var line = this.getLine();
            var index2 = this.data.index2;
            this.data.myAnswer = this.data.myAnswer || [];
            this.data.myAnswer.push({ status: this.timeout ? 0 : (isright ? 1 : -1), isright: isright, ti: line.tis[index2].tutu, answer: answer });
        }
    }
    //#endregion

    //#region 第12题正式显示位置
    class ShowLocPage12 extends BothQuestionPage11 {
        onload() {
            var data = $.extend({}, this.data);
            data.index2++;
            var line = this.getLine();
            setTimeout(function () {
                if (data.index2 < line.locs.length) {
                    toPage(new SymmetryQuestionPage12("symmetryTest_12", data));
                    return;
                }
                //global.setTimer(45);
                //data.showtimer = true;
                //data.BalanceSeconds = 45;
                toPage(new ShowLocAnswerPage12("locAnswer_12", data));

            }, 1000);
        }
    }
    //#endregion

    //#region 第12题回答记位置
    class ShowLocAnswerPage12 extends BothQuestionPage11 {
        timeout = false;
        myAnswer = [];
        funcs = {
            setvalue: function (e) {
                var page = global.localPage;
                $(page.el).find(".btnTop button").prop("disabled", false);
                var data = page.data;
                var line = page.getLine();
                if (page.myAnswer.length >= line.span) {
                    return;
                }
                var ballNo = page.myAnswer.length + 1;
                var url = "/Res/12/redgood" + ballNo + ".png";
                var parent = $(e.target).closest("td");
                $(e.target).hide();
                parent.append('<img src="' + url + '">');
                page.myAnswer.push(parent.attr("tid").replace("td_", ""));
            },
            clearBox: function (e) {
                var page = global.localPage;
                if (page.myAnswer.length <= 0) {

                    return;
                }
                var locNo = page.myAnswer.pop();
                var td = $(page.el).find("td[tid=td_" + locNo + "]");
                td.find("img").remove();
                td.find("input").show();
                if (page.myAnswer.length <= 0) {
                    $(page.el).find(".btnTop button").prop("disabled", true);
                }
            },
            checkBox: function (e) {
                let page = global.localPage;
                let data = page.data;
                let line = page.getLine();
                let res = line.locs.join('');
                let myRes = page.myAnswer.join("");
                let span = line.span;
                if (res == myRes) {
                    page.saveAnswer(true, myRes);
                } else {
                    page.saveAnswer(false, myRes);
                }
                if (data.test) {
                    page.processTest();
                    return;
                }
                page.processQuestion();
            }
        };
        onload() {
            
        }
        ontimer(second) {
            //$(this.el).find(".timerDiv_lb p font").html('剩余时间:' + second + '秒');
            //if (second <= 0) {
            //    this.timeout = true;
            //    $(this.el).find(".timerDiv_lb p font").html('超时');
            //}
        }
        saveAnswer(isright, answer) {
            var line = this.getLine();
            this.data.myAnswer = this.data.myAnswer || [];
            this.data.myAnswer.push({ status: this.timeout ? 0 : (isright ? 1 : -1), isright: isright, ti: line.locs.join(""), answer: answer });
        }
        getMyAnswer() {
            return this.data.myAnswer.sum((i, a) => a.status == 1 ? 1 : 0) / this.data.myAnswer.length;
        }
        processTest() {
            var score = this.getMyAnswer();
            if (score >= 0.9 || this.data.istest2) {
                this.data.myAnswer = null;
                this.data.test = null;
                toPage(new BothQuestionTip12("bothQuestionTip_12", this.data));
                return;
            }
            this.data.istest2 = true;
            this.data.myAnswer = null;
            toPage(new BothTestRetry12("bothTestRetry_11", this.data));
        }
        processQuestion() {
            var bili = this.getMyAnswer();
            var index = this.data.index;
            var line = this.getLine();
            var nextIndex
            var score = this.getMyAnswer();
            if (score >= 0.9) {
                nextIndex = this.data.lines.findIndex((a, i) => {
                    return i > index && a.span > line.span;
                });
            } else {
                nextIndex = this.data.lines.findIndex((a, i) => {
                    return i > index && a.span == line.span;
                });
            }
            if (nextIndex < 0) {
                toPage(new FinishOverPage("finishOver", {}));
                return;
            }
            if (score < 0.9) {
                this.data.spanid++;
            }
            this.data.span = this.data.lines[nextIndex].span;
            this.data.myAnswer = null;
            this.data.index = nextIndex;
            toPage(new ShowBothTitlePage12("bothTestTitle_12", this.data));
        }
    }
    //#endregion

    //#region 第12题显示重试
    class BothTestRetry12 extends SinglePage {
        onload() {
            var data = $.extend({}, this.data);
            setTimeout(function () {
                toPage(new ShowBothTitlePage12("bothTestTitle_12", data));
            }, 1000);
        }
    }
    //#endregion

    //#region 第12题正式显示双测
    class BothQuestionTip12 extends SinglePage {
        funcs = {
            start: function () {
                let page = global.localPage;
                var data = $.extend({}, page.data);
                toPage(new ShowBothTitlePage12("bothTestTitle_12", data));
            }
        };
    }
    //#endregion

    //#endregion

    //#region 第21题

    //#region 第21题第一页
    class FirstPage21 extends SinglePage {
        funcs = {
            start: function () {
                toPage(new PhotoTip_21("photoTip_21", {  }));
            }
        }
    }
    //#endregion


    //#region 第21题第一页
    class PhotoTip_21 extends SinglePage {
        funcs = {
            start: async function () {
                var example = plan.BusTestPlanExamples[global.localExampleIndex].Example;
                var url = "/Res/21/study.txt";
                var url2 = "/Res/21/test.txt";
                if (example.Title.indexOf("乙") > 0) {
                    url = "/Res/21/study2.txt";
                    url2 = "/Res/21/test2.txt";
                }
                var testtxt = await (await fetch(url)).text();
                var testtxt2 = await (await fetch(url2)).text();
                var lines = testtxt.split("\r\n").filter((a, i) => i > 0 && a).map(a => a.split(/\s+/));
                var tests = testtxt2.split("\r\n").filter((a, i) => i > 0 && a).map(a => a.split(/\s+/));
                //console.log(lines);
                toPage(new PhotoShow_21("photoShow_21", { lines: lines, tests: tests, index: 0,study:1,answerNo:0 }));
            }
        }
    }
    //#endregion

    


    //#region 第21题展示照片
    class PhotoShow_21 extends SinglePage {
        onload() {
            var data = $.extend({}, this.data);

            setTimeout(function () {
                data.index++;

                if (data.lines[data.index] && data.lines[data.index][0] == data.study) {
                    toPage(new PhotoShow_21("photoShow_21", data));
                    return;
                }

                if (data.answerNo == 0) {
                    
                    toPage(new AnswerTip_21("answerTip_21", data));
                    return;
                }

                toPage(new AnswerTip2_21("answerTip2_21", data));

            }, 12000);
            
        }
    }
    //#endregion

    //#region 第21题答题开始提示
    class AnswerTip_21 extends SinglePage {
        funcs = {
            start: function () {
                var data = global.localPage.data;
                global.setTimer(45);
                toPage(new Answer_21("answer_21", data));
            }
        };
    }
    //#endregion

    //#region 第21题答题开始提示2
    class AnswerTip2_21 extends SinglePage {
        funcs = {
            start: function () {
                var data = global.localPage.data;
                global.setTimer(45);
                toPage(new Answer_21("answer_21", data));
            }
        };
    }
    //#endregion

    //#region 第22题答题
    class Answer_21 extends SinglePage {
        funcs = {
            next: function () {
                global.localPage.next();
            },

            select: function (e) {
                var btn = $(e.target);
                var name = btn.attr("name");

                var parent = btn.closest(".tableBorder");

                parent.find("input[name=" + name + "]").removeClass("active");
                btn.addClass("active");
                parent.closest(".container").find("button.id_next").prop("disabled",false);
            }
        };
        ontimer(second) {
            $(this.el).find(".timerDiv p font").html('剩余时间:' + second + '秒');
            if (second <= 0) {
                this.next();
            }
        }
        next() {
            this.saveAnswer();
            let data = $.extend({}, this.data);

            data.answerNo++;
            let nextLine = data.tests[data.answerNo];

            if (!nextLine) {
                toPage(new FinishOverPage("finishOver", {}));
                return;
            }
            if (nextLine[0] != data.study) {
                data.study = nextLine[0];
                toPage(new PhotoTip2_21("photoTip2_21", data));
                return;
            }

            global.setTimer(45);
            toPage(new Answer_21("answer_21", data));
        }
        saveAnswer() {
            let data = this.data;
            data.myAnswer = data.myAnswer || [];

            let line = data.tests[data.answerNo];

            let myRes = $(this.el).find("input[name=name].active").val() + ">" + $(this.el).find("input[name=career].active").val() + ">" + $(this.el).find("input[name=hobby].active").val();
            let ret = line[2] + ">" + line[3] + ">" + line[4];
            let isright = myRes == ret;
            data.myAnswer.push({ status: isright ? 1 : -1, isright: isright, ti: line[1]+"："+ret, answer: myRes });
        }
    }
    //#endregion

    //#region 第21题再次呈现提示
    class PhotoTip2_21 extends SinglePage {
        funcs = {
            start: function () {
                var data = global.localPage.data;
                toPage(new PhotoShow_21("photoShow_21", data));
            }
        }
    }
    //#endregion


    //#endregion

    //#region 第22题

    //#region 第22题第一页
    class FirstPage22 extends SinglePage {
        funcs = {
            start: function () {
                toPage(new DemoTip_22("demoTip_22", {}));
            }
        }
    }
    //#endregion

    //#region 第22题第一页
    class DemoTip_22 extends SinglePage {
        funcs = {
            setvalue: function (e) {
                global.localPage.setvalue(e);
            },
            clearBox: function (e) {
                global.localPage.clear(e);
            },
            checkBox_learn: function (e) {
                global.localPage.confirm(e);
            }
        }
        myAnswer = [];
        clear(e) {
            var idx = this.myAnswer.length;
            var el = $(this.el).find(".span_que").eq(idx - 1);
            if (el.length <= 0) {
                return;
            }
            
            el.html("");
            this.myAnswer.pop();
            if (this.myAnswer.length <= 0) {
                $(this.el).find("button").prop("disabled", true);
            }
        }
        confirm(e) {
            if (this.myAnswer.join("") != "月亮") {
                return;
            }
            toPage(new WordTip_22("wordTip_22", {}));
        }
        setvalue(e) {
            var idx = this.myAnswer.length;
            var el = $(this.el).find(".span_que").eq(idx);
            if (el.length <= 0) {
                
                return;
            }
            $(this.el).find("button").prop("disabled", false);
            el.html($(e.target).val());
            this.myAnswer.push($(e.target).val());

            
        }
    }
    //#endregion

    //#region 第22题提示页
    class WordTip_22 extends SinglePage {
        funcs = {
            start: async function () {
                var example = plan.BusTestPlanExamples[global.localExampleIndex].Example;
                var url = "/Res/22/learn.txt";
                var url2 = "/Res/22/test.txt";
                if (example.Title.indexOf("乙") > 0) {
                    url = "/Res/22/learn2.txt";
                    url2 = "/Res/22/test2.txt";
                }
                var testtxt = await (await fetch(url)).text();
                var testtxt2 = await (await fetch(url2)).text();
                var lines = testtxt.split("\r\n").filter((a, i) => i > 0 && a).map(a => a.split(/\s+/));
                var tests = testtxt2.split("\r\n").filter((a, i) => i > 0 && a).map(a => a.split(/\s+/));
                //console.log(lines);
                toPage(new WordShow_22("wordShow_22", { lines: lines, tests: tests, index: 0, study: 1, answerNo: 0 }));
            }
        }
    }
    //#endregion

    //#region 第22题提示页
    class WordTip2_22 extends SinglePage {
        funcs = {
            start: async function () {
                toPage(new WordShow_22("wordShow_22", global.localPage.data));
            }
        }
    }
    //#endregion

    //#region 第22题展示词组
    class WordShow_22 extends SinglePage {
        onload() {
            var data = $.extend({}, this.data);

            setTimeout(function () {
                data.index++;

                if (data.lines[data.index] && data.lines[data.index][0] == data.study) {
                    toPage(new WordShow_22("wordShow_22", data));
                    return;
                }

                if (data.answerNo == 0) {

                    toPage(new AnswerTip_22("answerTip_21", data));
                    return;
                }

                toPage(new AnswerTip2_22("answerTip2_21", data));

            }, 3000);

        }
    }
    //#endregion

    //#region 第22题答题开始提示
    class AnswerTip_22 extends SinglePage {
        funcs = {
            start: function () {
                var data = global.localPage.data;
                global.setTimer(30);
                toPage(new Answer_22("answer_22", data));
            }
        };
    }
    //#endregion

    //#region 第22题答题开始提示2
    class AnswerTip2_22 extends SinglePage {
        funcs = {
            start: function () {
                var data = global.localPage.data;
                global.setTimer(30);
                toPage(new Answer_22("answer_22", data));
            }
        };
    }
    //#endregion

    //#region 第22题答题
    class Answer_22 extends SinglePage {
        myAnswer = [];
        funcs = {
            setvalue: function (e) {
                global.localPage.setvalue(e);
            },
            clearBox: function (e) {
                global.localPage.clear(e);
            },
            checkBox_Test: function (e) {
                global.localPage.confirm(e);
            }
        };
        ontimer(second) {
            $(this.el).find(".timerDiv p font").html('剩余时间:' + second + '秒');
            if (second <= 0) {
                this.next();
            }
        }
        next() {
            this.saveAnswer();
            let data = $.extend({}, this.data);

            data.answerNo++;
            let nextLine = data.tests[data.answerNo];

            if (!nextLine) {
                toPage(new FinishOverPage("finishOver", {}));
                return;
            }
            if (nextLine[0] != data.study) {
                data.study = nextLine[0];
                toPage(new WordTip2_22("photoTip2_21", data));
                return;
            }

            global.setTimer(30);
            toPage(new Answer_22("answer_22", data));
        }
        saveAnswer() {
            let data = this.data;
            data.myAnswer = data.myAnswer || [];

            let line = data.tests[data.answerNo];

            let myRes = this.myAnswer.join("");
            let ret = line[2];
            let isright = myRes == ret;
            data.myAnswer.push({ status: isright ? 1 : -1, isright: isright, ti: line[1] + "：" + ret, answer: myRes });
        }

        clear(e) {
            var idx = this.myAnswer.length;
            var el = $(this.el).find(".span_que").eq(idx - 1);
            if (el.length <= 0) {
                return;
            }

            el.html("");
            this.myAnswer.pop();
            if (this.myAnswer.length <= 0) {
                $(this.el).find("button").prop("disabled", true);
            }
        }
        confirm(e) {
            this.next();
        }
        setvalue(e) {
            var idx = this.myAnswer.length;
            var el = $(this.el).find(".span_que").eq(idx);
            if (el.length <= 0) {

                return;
            }
            $(this.el).find("button").prop("disabled", false);
            el.html($(e.target).val());
            this.myAnswer.push($(e.target).val());


        }
    }
    //#endregion

    //#endregion

    //#region 第31题

    //#region 第31题第一页
    class FirstPage31 extends SinglePage {
        funcs = {
            start: function () {
                toPage(new DemoTip_31("demoTip_31", {}));
            }
        }
    }
    //#endregion

    //#region 第31题第一页
    class DemoTip_31 extends SinglePage {
        funcs = {
            startPage: function (e) {
                toPage(new FirstPage31("first_31", {}));
            },
            startTest: function (e) {
                toPage(new PicTip_31("picTip_31", {}));
            },
            setvalue: function (e) {
                global.localPage.setvalue(e);
            },
        }
        beforeLoad() {
            this.funcs.q1 = "/Res/31/q1.png";
            this.funcs.q2 = "/Res/31/q2.png";
            this.funcs.o1 = "/Res/31/o1.png";
            this.funcs.o2 = "/Res/31/o2.png";
            this.funcs.o3 = "/Res/31/o3.png";
            this.funcs.o4 = "/Res/31/o4.png";
            this.funcs.o5 = "/Res/31/o5.png";
        }
        setvalue(e) {
            $(this.el).find("img[imgno]").removeClass("active");
            $(e.target).addClass("active");
            let isright = $(e.target).attr("imgno") == "3";
            if (isright) {
                $(this.el).find(".clickMouse span").text("正确").css("color", "green");
            } else {
                $(this.el).find(".clickMouse span").text("错误，正确答案是第3项").css("color", "red");
            }
            $(this.el).find(".id_next1").prop("disabled", false);
        }
    }
    //#endregion

    //#region 第31题图片说明
    class PicTip_31 extends SinglePage {
        funcs = {
            start: async function () {
                var example = plan.BusTestPlanExamples[global.localExampleIndex].Example;
                var url = "/Res/31/test.txt";
                if (example.Title.indexOf("乙") > 0) {
                    url = "/Res/31/test2.txt";
                }
                var testtxt = await(await fetch(url)).text();
                var lines = testtxt.split("\r\n").filter((a, i) => i > 0 && a).map(a => a.split(/\s+/));
                //console.log(lines);
                toPage(new Answer_31("answer_31", { lines: lines, index: 0 }));
            }
        }
    }
    //#endregion

    //#region 第31题图片说明
    class Answer_31 extends SinglePage {
        isanswer = false;
        timeout = false;
        funcs = {
            next: function (e) {
                global.localPage.next(e);
            },
            setvalue: function (e) {
                global.localPage.setvalue(e);
            },
        }
        beforeLoad() {
            var line = this.getLine();
            this.funcs.q1 = "/Res/31/"+line[2]+".png";
            this.funcs.q2 = "/Res/31/" + line[3] +".png";
            this.funcs.o1 = "/Res/31/" + line[7] +".png";
            this.funcs.o2 = "/Res/31/" + line[8] +".png";
            this.funcs.o3 = "/Res/31/" + line[9] +".png";
            this.funcs.o4 = "/Res/31/" + line[10] +".png";
            this.funcs.o5 = "/Res/31/" + line[11] + ".png";
            this.funcs.duration = 45;
            this.funcs.answer = line[12];
            if (line[4] != "#") {
                this.funcs.duration = 75;
                this.funcs.q3 = "/Res/31/" + line[4] + ".png";
            }
            if (line[5] != "#") {
                this.funcs.duration = 120;
                this.funcs.q4 = "/Res/31/" + line[5] + ".png";
            }
            global.setTimer(this.funcs.duration);
        }
        setvalue(e) {
            $(this.el).find("img[imgno]").removeClass("active");
            $(e.target).addClass("active");
            $(this.el).find(".id_next1").prop("disabled", false);
        }
        next() {
            if (this.isanswer) {
                this.data.index++;
                if (this.data.index >= this.data.lines.length || this.get5error()) {
                    toPage(new FinishOverPage("finishOver", {}));
                    return;
                }
                toPage(new Answer_31("answer_31", this.data));
                return;
            }
            $(this.el).find(".id_next1").prop("disabled", false);
            this.isanswer = true;
            var ret = this.data.answer;
            var myRes = $(this.el).find("img[imgno].active").attr("imgno");
            var isright = ret == myRes;
            this.saveAnswer(isright, myRes);
            if (this.data.index < 4 && (!isright || this.timeout)) {
                //报错
                $(this.el).find(".Inaccurate font").html((this.timeout ? "超时" : "错误") + $(this.el).find(".Inaccurate").css("visibility", "visible").find("font").html());
                $(this.el).find(".timerDiv").css("visibility","hidden");
                var that = this;
                setTimeout(function () {
                    if (that.isClose) return;
                    that.next();
                }, 3000);
                return;
            }
            this.data.index++;
            if (this.data.index >= this.data.lines.length || this.get5error()) {
                toPage(new FinishOverPage("finishOver", {}));
                return;
            }

            

            toPage(new Answer_31("answer_31", this.data));
        }
        get5error() {
            let last5 = this.data.myAnswer.filter((a, i) => i >= (this.data.myAnswer.length - 5));
            let error5 = last5.filter(a => a.status != 1).length;
            if (last5.length == 5 && error5 == 5) {
                return true;
            }
            return false;
        }
        ontimer(second) {
            $(this.el).find(".timerDiv p font").html('剩余时间:' + second + '秒');
            if (second <= 0 && !this.isanswer) {
                this.timeout = true;
                this.next();
            }
        }

        getLine() {
            return this.data.lines[this.data.index];
        }
        saveAnswer(isright, answer) {
            var line = this.getLine();
            this.data.myAnswer = this.data.myAnswer || [];
            this.data.myAnswer.push({ status: this.timeout ? 0 : (isright ? 1 : -1), isright: isright, ti: line[0]+":"+line[12], answer: answer });
        }
    }
    //#endregion

    //#endregion

    //#region 第32题

    //#region 第32题第一页
    class FirstPage32 extends SinglePage {
        funcs = {
            start: function () {
                toPage(new DemoTip_32("demoTip_32", { demono :1}));
            }
        }
    }
    //#endregion

    //#region 第32题第一页
    class DemoTip_32 extends SinglePage {
        funcs = {
            startPage: function (e) {
                if (global.localPage.data.demono == 1) {
                    toPage(new FirstPage32("first_32", {}));
                } else {
                    toPage(new PicTip_32("picTip_32", {}));
                }
                
            },
            startTest: function (e) {
                if (global.localPage.data.demono == 1) {
                    toPage(new PicTip_32("picTip_32", {}));
                } else {
                    toPage(new PicTip2_32("picTip2_32", {}));
                }
                
            },
            setvalue: function (e) {
                global.localPage.setvalue(e);
            },
        }
        beforeLoad() {
            if (this.data.demono == 1) {
                this.funcs.q1 = "/Res/32/example/q1.png";
                this.funcs.q2 = "/Res/32/example/q2.png";
                this.funcs.o1 = "/Res/32/example/o1.png";
                this.funcs.o2 = "/Res/32/example/o2.png";
                this.funcs.o3 = "/Res/32/example/o3.png";
                this.funcs.o4 = "/Res/32/example/o4.png";
                this.funcs.o5 = "/Res/32/example/o5.png";
            } else {
                this.funcs.q1 = "/Res/32/example2/q1.png";
                this.funcs.q2 = "/Res/32/example2/q2.png";
                this.funcs.o1 = "/Res/32/example2/o1.png";
                this.funcs.o2 = "/Res/32/example2/o2.png";
                this.funcs.o3 = "/Res/32/example2/o3.png";
                this.funcs.o4 = "/Res/32/example2/o4.png";
                this.funcs.o5 = "/Res/32/example2/o5.png";
            }
            
        }
        setvalue(e) {
            $(this.el).find("img[imgno]").removeClass("active");
            $(e.target).addClass("active");
            let isright = $(e.target).attr("imgno") == "1";
            if (isright) {
                $(this.el).find(".clickMouse span").text("正确").css("color", "green");
            } else {
                $(this.el).find(".clickMouse span").text("错误，正确答案是第1项").css("color", "red");
            }
            
            $(this.el).find(".id_next1").prop("disabled", false);
        }
    }
    //#endregion

    //#region 第32题图片说明
    class PicTip_32 extends SinglePage {
        funcs = {
            back: function () {
                toPage(new DemoTip_32("demoTip_32", { demono: 1 }));
            },
            next: function () {
                toPage(new DemoTip_32("demoTip2_32", { demono: 2 }));
            }
        }
    }
    //#endregion

    //#region 第32题图片说明
    class PicTip2_32 extends SinglePage {
        funcs = {
            back: function () {
                toPage(new DemoTip_32("demoTip2_32", { demono: 2 }));
            },
            start: async function () {
                var example = plan.BusTestPlanExamples[global.localExampleIndex].Example;
                var url = "/Res/32/cube.txt";
                if (example.Title.indexOf("乙") > 0) {
                    url = "/Res/32/cube2.txt";
                }
                var testtxt = await (await fetch(url)).text();
                var lines = testtxt.split("\r\n").filter((a, i) => i > 0 && a).map(a => a.split(/\s+/));
                //console.log(lines);
                toPage(new Answer_32("answer_32", { lines: lines, index: 0 }));
            }
        }
    }
    //#endregion

    //#region 第32题图片说明
    class Answer_32 extends SinglePage {
        isanswer = false;
        timeout = false;
        funcs = {
            next: function (e) {
                global.localPage.next(e);
            },
            setvalue: function (e) {
                global.localPage.setvalue(e);
            },
        }
        beforeLoad() {
            var line = this.getLine();
            this.funcs.q1 = "/Res/32/" + line[2] + ".png";
            this.funcs.q2 = "/Res/32/" + line[3] + ".png";
            this.funcs.o1 = "/Res/32/" + line[7] + ".png";
            this.funcs.o2 = "/Res/32/" + line[8] + ".png";
            this.funcs.o3 = "/Res/32/" + line[9] + ".png";
            this.funcs.o4 = "/Res/32/" + line[10] + ".png";
            this.funcs.o5 = "/Res/32/" + line[11] + ".png";
            this.funcs.duration = 45;
            this.funcs.answer = line[12];
            if (line[4] != "#") {
                this.funcs.duration = 90;
                this.funcs.q3 = "/Res/32/" + line[4] + ".png";
            }
            if (line[5] != "#") {
                this.funcs.duration = 120;
                this.funcs.q4 = "/Res/32/" + line[5] + ".png";
            }
            global.setTimer(this.funcs.duration);
        }
        setvalue(e) {
            $(this.el).find("img[imgno]").removeClass("active");
            $(e.target).addClass("active");
            $(this.el).find(".id_next1").prop("disabled", false);
        }
        next() {
            if (this.isanswer) {
                this.data.index++;
                if (this.data.index >= this.data.lines.length || this.get5error()) {
                    toPage(new FinishOverPage("finishOver", {}));
                    return;
                }
                toPage(new Answer_32("answer_32", this.data));
                return;
            }
            $(this.el).find(".id_next1").prop("disabled", false);
            this.isanswer = true;
            var ret = this.data.answer;
            var myRes = $(this.el).find("img[imgno].active").attr("imgno");
            var isright = ret == myRes;
            this.saveAnswer(isright, myRes);
            if (this.data.index < 4 && (!isright || this.timeout)) {
                //报错
                $(this.el).find(".Inaccurate font").html((this.timeout ? "超时" : "错误") + $(this.el).find(".Inaccurate").css("visibility", "visible").find("font").html());
                $(this.el).find(".timerDiv").css("visibility", "hidden");
                var that = this;
                setTimeout(function () {
                    if (that.isClose) return;
                    that.next();
                }, 3000);
                return;
            }
            this.data.index++;
            if (this.data.index >= this.data.lines.length || this.get5error()) {
                toPage(new FinishOverPage("finishOver", {}));
                return;
            }



            toPage(new Answer_32("answer_32", this.data));
        }
        get5error() {
            let last5 = this.data.myAnswer.filter((a, i) => i >= (this.data.myAnswer.length - 5));
            let error5 = last5.filter(a => a.status != 1).length;
            if (last5.length == 5 && error5 == 5) {
                return true;
            }
            return false;
        }
        ontimer(second) {
            $(this.el).find(".timerDiv p font").html('剩余时间:' + second + '秒');
            if (second <= 0 && !this.isanswer) {
                this.timeout = true;
                this.next();
            }
        }

        getLine() {
            return this.data.lines[this.data.index];
        }
        saveAnswer(isright, answer) {
            var line = this.getLine();
            this.data.myAnswer = this.data.myAnswer || [];
            this.data.myAnswer.push({ status: this.timeout ? 0 : (isright ? 1 : -1), isright: isright, ti: line[0] + ":" + line[12], answer: answer });
        }
    }
    //#endregion

    //#endregion

    //#region 第41题

    //#region 第41题第一页
    class FirstPage41 extends SinglePage {
        funcs = {
            start: async function () {
                var example = plan.BusTestPlanExamples[global.localExampleIndex].Example;
                var url = "/Res/41/FEITSWsource.txt";
                
                var testtxt = await(await fetch(url)).text();
                var lines = testtxt.split("\r\n").filter((a, i) => i > 0 && a).map(a => a.split(/\s+/));
                //console.log(lines);
                toPage(new Answer_41("answer_41", { lines: lines, index: 0 }));
            }
        }
    }
    //#endregion

    //#region 第41题第一页
    class Answer_41 extends SinglePage {
        isanswer = false;
        timeout = false;
        funcs = {
            next: function (e) {
                global.localPage.next(e);
            },
            setvalue: function (e) {
                global.localPage.setvalue(e);
            },
        }
        beforeLoad() {
            global.setTimer(30);
            this.data.answer = (this.getLine().findIndex((a, i) => i >= 7 && a == "2") - 6)+"";
        }
        setvalue(e) {
            $(this.el).find("button[no]").removeClass("active");
            $(e.target).addClass("active");
            $(this.el).find(".id_next").prop("disabled", false);
        }
        next() {
            if (this.isanswer) {
                this.data.index++;
                if (this.data.index >= this.data.lines.length || this.get5error()) {
                    toPage(new FinishOverPage("finishOver", {}));
                    return;
                }
                toPage(new Answer_41("answer_41", this.data));
                return;
            }
            $(this.el).find(".id_next").prop("disabled", false);
            this.isanswer = true;
            var ret = this.data.answer;
            var myRes = $(this.el).find("button[no].active").attr("no");
            var isright = ret == myRes;
            var score = $(this.el).find("button[no].active").attr("score");
            if (score == null) {
                score = 0;
            } else {
                score = parseInt(score);
            }
            this.saveAnswer(isright, myRes, score);
            if (this.data.index < 4 && (!isright || this.timeout)) {
                //报错
                $(this.el).find(".Inaccurate font").html((this.timeout ? "超时" : (score<=0?"错误":"不准确")) + $(this.el).find(".Inaccurate").css("visibility", "visible").find("font").html());
                $(this.el).find(".timerDiv").css("visibility", "hidden");
                var that = this;
                setTimeout(function () {
                    if (that.isClose) return;
                    that.next();
                }, 3000);
                return;
            }
            this.data.index++;
            if (this.data.index >= this.data.lines.length || this.get5error()) {
                toPage(new FinishOverPage("finishOver", {}));
                return;
            }



            toPage(new Answer_41("answer_41", this.data));
        }
        get5error() {
            let last5 = this.data.myAnswer.filter((a, i) => i >= (this.data.myAnswer.length - 5));
            let error5 = last5.filter(a => a.status != 1).length;
            if (last5.length == 5 && error5 == 5) {
                return true;
            }
            return false;
        }
        ontimer(second) {
            $(this.el).find(".timerDiv p font").html('剩余时间:' + second + '秒');
            if (second <= 0 && !this.isanswer) {
                this.timeout = true;
                this.next();
            }
        }

        getLine() {
            return this.data.lines[this.data.index];
        }
        saveAnswer(isright, answer,score) {
            var line = this.getLine();
            this.data.myAnswer = this.data.myAnswer || [];
            this.data.myAnswer.push({ status: this.timeout ? 0 : (isright ? 1 : -1), isright: isright, ti: line[0] + ":" + line[12], answer: answer, score: score });
        }
    }
    //#endregion


    //#endregion

    //#region 第42题

    //#region 第42题第一页
    class FirstPage42 extends SinglePage {
        funcs = {
            start: async function () {
                var example = plan.BusTestPlanExamples[global.localExampleIndex].Example;
                var url = "/Res/42/FEITCOMMsource.txt";

                var testtxt = await (await fetch(url)).text();
                var lines = testtxt.split("\r\n").filter((a, i) => i > 0 && a).map(a => a.split(/\s+/));
                //console.log(lines);
                toPage(new Answer_42("answer_41", { lines: lines, index: 0 }));
            }
        }
    }
    //#endregion

    //#region 第42题第一页
    class Answer_42 extends SinglePage {
        isanswer = false;
        timeout = false;
        funcs = {
            next: function (e) {
                global.localPage.next(e);
            },
            setvalue: function (e) {
                global.localPage.setvalue(e);
            },
        }
        beforeLoad() {
            global.setTimer(30);
            this.data.answer = (this.getLine().findIndex((a, i) => i >= 7 && a == "2") - 6) + "";
        }
        setvalue(e) {
            $(this.el).find("button[no]").removeClass("active");
            $(e.target).addClass("active");
            $(this.el).find(".id_next").prop("disabled", false);
        }
        next() {
            if (this.isanswer) {
                this.data.index++;
                if (this.data.index >= this.data.lines.length || this.get5error()) {
                    toPage(new FinishOverPage("finishOver", {}));
                    return;
                }
                toPage(new Answer_42("answer_41", this.data));
                return;
            }
            $(this.el).find(".id_next").prop("disabled", false);
            this.isanswer = true;
            var ret = this.data.answer;
            var myRes = $(this.el).find("button[no].active").attr("no");
            var isright = ret == myRes;
            var score = $(this.el).find("button[no].active").attr("score");
            if (score == null) {
                score = 0;
            } else {
                score = parseInt(score);
            }
            this.saveAnswer(isright, myRes, score);
            if (this.data.index < 4 && (!isright || this.timeout)) {
                //报错
                $(this.el).find(".Inaccurate font").html((this.timeout ? "超时" : (score <= 0 ? "错误" : "不准确")) + $(this.el).find(".Inaccurate").css("visibility", "visible").find("font").html());
                $(this.el).find(".timerDiv").css("visibility", "hidden");
                var that = this;
                setTimeout(function () {
                    if (that.isClose) return;
                    that.next();
                }, 3000);
                return;
            }
            this.data.index++;
            if (this.data.index >= this.data.lines.length || this.get5error()) {
                toPage(new FinishOverPage("finishOver", {}));
                return;
            }



            toPage(new Answer_42("answer_41", this.data));
        }
        get5error() {
            let last5 = this.data.myAnswer.filter((a, i) => i >= (this.data.myAnswer.length - 5));
            let error5 = last5.filter(a => a.status != 1).length;
            if (last5.length == 5 && error5 == 5) {
                return true;
            }
            return false;
        }
        ontimer(second) {
            $(this.el).find(".timerDiv p font").html('剩余时间:' + second + '秒');
            if (second <= 0 && !this.isanswer) {
                this.timeout = true;
                this.next();
            }
        }

        getLine() {
            return this.data.lines[this.data.index];
        }
        saveAnswer(isright, answer, score) {
            var line = this.getLine();
            this.data.myAnswer = this.data.myAnswer || [];
            this.data.myAnswer.push({ status: this.timeout ? 0 : (isright ? 1 : -1), isright: isright, ti: line[0] + ":" + line[12], answer: answer, score: score });
        }
    }
    //#endregion


    //#endregion

    //#region 结束页
    class FinishOverPage extends SinglePage {
        onload() {
            setTimeout(function () {
                toExampleStart(global.localExampleIndex+1);
            }, 3000);
        }
    }
    //#endregion

    //#region 键盘事件
    $(document).on("keydown", function (e) {
        if (!global.localPage) return;
        switch (e.keyCode) {
            case 32: {
                if (typeof global.localPage.onSpaceDown == "function") {
                    global.localPage.onSpaceDown(e);
                }
            } break;
            case 70: {
                if (typeof global.localPage.onFDown == "function") {
                    global.localPage.onFDown(e);
                }
            } break;
            case 74: {
                if (typeof global.localPage.onJDown == "function") {
                    global.localPage.onJDown(e);
                }
            } break;
        }
    });
    //#endregion

    //#region 缩放事件
    $(window).on("resize", function (e) {
        if (!global.localPage) return;
        if (typeof global.localPage.onresize == "function") {
            global.localPage.onresize(e);
        }
    });
    //#endregion

    //#region 计时器
    global.timer = setInterval(function () {
        if (global.BalanceSeconds == null || global.BalanceSeconds < 0) {
            return;
        }
        if (!global.localPage) {
            return;
        }
        if (typeof global.localPage.ontimer != "function") {
            return;
        }
        global.localPage.ontimer(global.BalanceSeconds);
        global.BalanceSeconds--;
    }, 1000);
    //#endregion

    //#region 关闭单页
    function toClose() {
        if (global.localPage) {
            global.localPage.close();
            global.localPage = null;
        }
    }
    //#endregion

    //#region 单页跳转
    function toPage(page) {
        toClose();
        global.localPage = page.init();
    }
    //#endregion

    //#region 退出单页
    function toExit() {
        $$.closeThis();
    }
    //#endregion

    //#region 跳转到第一页
    function toExampleStart(index) {
        global.localExampleIndex = index;
        var example = plan.BusTestPlanExamples[index];
        if (!example) {
            toExit();
        }
        global.localQuestionIndex = 0;
        global.localExampleType = example.Example.SpecialType;
        includeCss(global.localExampleType);
        switch (global.localExampleType) {
            case 1: {
                
                toPage(new FirstPage1("first_" + global.localExampleType, {}));
            } break;
            case 2: {
                toPage(new FirstPage2("first_" + global.localExampleType, {}));
            } break;

            case 11: {
                toPage(new FirstPage11("first_" + global.localExampleType, {}));
                //toPage(new BothTipPage11("bothTestTip_11", {}));
            } break;
            case 12: {
                toPage(new FirstPage12("first_" + global.localExampleType, {}));
                //toPage(new BothTipPage12("bothTestTip_12", {}));
            } break;
            
            case 21: {
                toPage(new FirstPage21("first_" + global.localExampleType, {}));
            } break;
            case 22: {
                toPage(new FirstPage22("first_" + global.localExampleType, {}));
            } break;
            
            case 31: {
                toPage(new FirstPage31("first_" + global.localExampleType, {}));
            } break;
            case 32: {
                toPage(new FirstPage32("first_" + global.localExampleType, {}));
            } break;
            
            case 41: {
                toPage(new FirstPage41("first_" + global.localExampleType, {}));
            } break;
            case 42: {
                toPage(new FirstPage42("first_" + global.localExampleType, {}));
            } break;
        }
    }
    //#endregion

    //#region 动态样式插入
    function includeCss(number) {
        $("#styleId").remove();
        $("head").append('<link id="styleId" rel="stylesheet" href="/Res/' + number + '/style.css" />');
    }
    //#endregion

    //#region 初始化
    async function init() {
        var json = await $$.get(BaseUrl + "/GetTest/" + PageInfo.KeyValueStr);
        let plan = json.data;
        window.plan = plan;

        toPage(new MainPage("welcome",{}));
    }
    //#endregion
    
    init();
});
