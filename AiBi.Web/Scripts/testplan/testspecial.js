﻿var global = {
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
            this.data = $.extend(this.data,this.funcs);
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

        answer(exist) {
            if (this.isanswer) return;
            this.isanswer = true;
            if (this.data.question.exist == exist) {
                console.log("正确");
                TestPage1.setAnswer({ score: 1, result: exist });
                this.toNext();
            } else {
                console.log("错误");
                TestPage1.setAnswer({ score: 0, result: exist });
                $(this.el).find(".msgDiv").show();
                var that = this;
                setTimeout(function () {
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
            toPage(new FinishOverPage("finishOver", {}));
        }
    }
    //#endregion

    //#region 第一题测试结束页
    class TestEndPage1 extends SinglePage {
        funcs = {
            onprev: function (e) {
                TestPage1.setAnswer();
                toPage(new TestPage1("question_1", TestPage1.getImgData(0, TestEndPage1.lines)));
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

    //#region 结束页
    class FinishOverPage extends SinglePage {
        onload() {
            setTimeout(function () {
                toExampleStart(global.localExampleIndex+1);
            }, 3000);
        }
    }
    //#endregion

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
    $(window).on("resize", function (e) {
        if (!global.localPage) return;
        if (typeof global.localPage.onresize == "function") {
            global.localPage.onresize(e);
        }
    });
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

    function toClose() {
        if (global.localPage) {
            global.localPage.close();
            global.localPage = null;
        }
    }
    function toPage(page) {
        toClose();
        global.localPage = page.init();
    }
    function toExit() {
        $$.closeThis();
    }

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
            case 12: { } break;
            
            case 21: { } break;
            case 22: { } break;
            
            case 31: { } break;
            case 32: { } break;
            
            case 41: { } break;
            case 42: { } break;
        }
    }
    function includeCss(number) {
        $("#styleId").remove();
        $("head").append('<link id="styleId" rel="stylesheet" href="/Res/' + number + '/style.css" />');
    }
    async function init() {
        var json = await $$.get(BaseUrl + "/GetTest/" + PageInfo.KeyValueStr);
        let plan = json.data;
        window.plan = plan;

        toPage(new MainPage("welcome",{}));
    }
    init();
});
