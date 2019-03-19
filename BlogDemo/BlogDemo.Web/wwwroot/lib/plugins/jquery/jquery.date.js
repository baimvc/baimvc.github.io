/**
 * Title��������ط��� 
 * Author������С��ͬѧ 
 * http://www.cnblogs.com/liuxianan/p/js-date-format-parse.html
 */
; (function ($) {
    $.extend(
    {
        /**
         * �����ڸ�ʽ����ָ����ʽ���ַ���
         * @param date Ҫ��ʽ�������ڣ�����ʱĬ�ϵ�ǰʱ�䣬Ҳ������һ��ʱ���
         * @param fmt Ŀ���ַ�����ʽ��֧�ֵ��ַ��У�y,M,d,q,w,H,h,m,S��Ĭ�ϣ�yyyy-MM-dd HH:mm:ss
         * @returns ���ظ�ʽ����������ַ���
		 * 
		 * ʾ����
    	 * formatDate(); // 2016-09-02 13:17:13
		 * formatDate(1472793615764); // 2016-09-02 13:20:15
    	 * formatDate(new Date(), 'yyyy-MM-dd'); // 2016-09-02
    	 * formatDate(new Date(), 'yyyy-MM-dd ��q���� www HH:mm:ss:SSS'); // 2016-09-02 ��3���� ������ 13:19:15:792
         */
        formatDate: function (date, fmt) {
            date = date == undefined ? new Date() : date;
            date = typeof date == 'number' ? new Date(date) : date;
            fmt = fmt || 'yyyy-MM-dd HH:mm:ss';
            var obj =
            {
                'y': date.getFullYear(), // ��ݣ�ע�������getFullYear
                'M': date.getMonth() + 1, // �·ݣ�ע���Ǵ�0-11
                'd': date.getDate(), // ����
                'q': Math.floor((date.getMonth() + 3) / 3), // ����
                'w': date.getDay(), // ���ڣ�ע����0-6
                'H': date.getHours(), // 24Сʱ��
                'h': date.getHours() % 12 == 0 ? 12 : date.getHours() % 12, // 12Сʱ��
                'm': date.getMinutes(), // ����
                's': date.getSeconds(), // ��
                'S': date.getMilliseconds() // ����
            };
            var week = ['��', 'һ', '��', '��', '��', '��', '��'];
            for (var i in obj) {
                fmt = fmt.replace(new RegExp(i + '+', 'g'), function (m) {
                    var val = obj[i] + '';
                    if (i == 'w') return (m.length > 2 ? '����' : '��') + week[val];
                    for (var j = 0, len = val.length; j < m.length - len; j++) val = '0' + val;
                    return m.length == 1 ? val : val.substring(val.length - m.length);
                });
            }
            return fmt;
        },
        /**
         * ���ַ�������������
         * @param str ����������ַ�������'2014-09-13'
         * @param fmt �ַ�����ʽ��Ĭ��'yyyy-MM-dd'��֧�����£�y��M��d��H��m��s��S����֧��w��q
         * @returns �������Date��������
		 *
		 * ʾ����
		 * parseDate('2016-08-11'); // Thu Aug 11 2016 00:00:00 GMT+0800
		 * parseDate('2016-08-11 13:28:43', 'yyyy-MM-dd HH:mm:ss') // Thu Aug 11 2016 13:28:43 GMT+0800
         */
        parseDate: function (str, fmt) {
            fmt = fmt || 'yyyy-MM-dd';
            var obj = { y: 0, M: 1, d: 0, H: 0, h: 0, m: 0, s: 0, S: 0 };
            fmt.replace(/([^yMdHmsS]*?)(([yMdHmsS])\3*)([^yMdHmsS]*?)/g, function (m, $1, $2, $3, $4, idx, old) {
                str = str.replace(new RegExp($1 + '(\\d{' + $2.length + '})' + $4), function (_m, _$1) {
                    obj[$3] = parseInt(_$1);
                    return '';
                });
                return '';
            });
            obj.M--; // �·��Ǵ�0��ʼ�ģ�����Ҫ��ȥ1
            var date = new Date(obj.y, obj.M, obj.d, obj.H, obj.m, obj.s);
            if (obj.S !== 0) date.setMilliseconds(obj.S); // ��������˺���
            return date;
        },
        /**
         * ��һ�����ڸ�ʽ�����Ѻø�ʽ�����磬1�������ڵķ��ء��ոա���
         * ����ķ���ʱ�֣�����ķ������գ����򣬷���������
         * @param {Object} date
         */
        formatDateToFriendly: function (date) {
            date = date || new Date();
            date = typeof date === 'number' ? new Date(date) : date;
            var now = new Date();
            if ((now.getTime() - date.getTime()) < 60 * 1000) return '�ո�'; // 1���������������ոա�
            var temp = this.formatDate(date, 'yyyy��M��d');
            if (temp == this.formatDate(now, 'yyyy��M��d')) return this.formatDate(date, 'HH:mm');
            if (date.getFullYear() == now.getFullYear()) return this.formatDate(date, 'M��d��');
            return temp;
        },
        /**
         * ��һ��ʱ��ת�����Ѻø�ʽ���磺
         * 147->��2��27�롱
         * 1581->��26��21�롱
         * 15818->��4Сʱ24�֡�
         * @param {Object} second
         */
        formatDurationToFriendly: function (second) {
            if (second < 60) return second + '��';
            else if (second < 60 * 60) return (second - second % 60) / 60 + '��' + second % 60 + '��';
            else if (second < 60 * 60 * 24) return (second - second % 3600) / 60 / 60 + 'Сʱ' + Math.round(second % 3600 / 60) + '��';
            return (second / 60 / 60 / 24).toFixed(1) + '��';
        },
        /** 
         * ��ʱ��ת����MM:SS��ʽ 
         */
        formatTimeToFriendly: function (second) {
            var m = Math.floor(second / 60);
            m = m < 10 ? ('0' + m) : m;
            var s = second % 60;
            s = s < 10 ? ('0' + s) : s;
            return m + ':' + s;
        },
        /**
         * �ж�ĳһ���Ƿ�������
         * @param year ������һ��date���ͣ�Ҳ������һ��int���͵���ݣ�����Ĭ�ϵ�ǰʱ��
         */
        isLeapYear: function (year) {
            if (year === undefined) year = new Date();
            if (year instanceof Date) year = year.getFullYear();
            return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
        },
        /**
         * ��ȡĳһ��ĳһ�µ���������û���κβ���ʱ��ȡ��ǰ�·ݵ�
         * ��ʽһ��getMonthDays();
         * ��ʽ����getMonthDays(new Date());
         * ��ʽ����getMonthDays(2013, 12);
         */
        getMonthDays: function (date, month) {
            var y, m;
            if (date == undefined) date = new Date();
            if (date instanceof Date) {
                y = date.getFullYear();
                m = date.getMonth();
            }
            else if (typeof date == 'number') {
                y = date;
                m = month - 1;
            }
            var days = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31]; // �������һ����ÿ���·ݵ�����
            //��������겢����2��
            if (m == 1 && this.isLeapYear(y)) return days[m] + 1;
            return days[m];
        },
        /**
         * ����2����֮����������õ��ǱȽϺ������ķ���
         * ������������Ҫô��Date���ͣ�Ҫô��yyyy-MM-dd��ʽ���ַ�������
         * @param date1 ����һ
         * @param date2 ���ڶ�
         */
        countDays: function (date1, date2) {
            var fmt = 'yyyy-MM-dd';
            // ������ת�����ַ�����ת����Ŀ����ȥ����ʱ���֡��롱
            if (date1 instanceof Date && date2 instanceof Date) {
                date1 = this.format(fmt, date1);
                date2 = this.format(fmt, date2);
            }
            if (typeof date1 === 'string' && typeof date2 === 'string') {
                date1 = this.parse(date1, fmt);
                date2 = this.parse(date2, fmt);
                return (date1.getTime() - date2.getTime()) / (1000 * 60 * 60 * 24);
            }
            else {
                console.error('������ʽ��Ч��');
                return 0;
            }
        },

        /** 
         * ����1970��1��1����ҹ��ĺ�������ʾ�����ڸ�ʽ  Created by gaoyang on 2016/9/4
         * @param cellval ������
         */
        cellvalToDate: function (cellval) {
            var date = new Date(parseInt(cellval.replace("/Date(", "").replace(")/", ""), 10));
            var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
            var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
            return date.getFullYear() + "-" + month + "-" + currentDate;
        },
		
		/**
         * ��ȡ����ǰN����N�������  Created by gaoyang on 2016/9/4
         * @param n ����
         */
        showDate: function (n) {
            var uom = new Date(new Date() - 0 + n * 86400000);
            uom = uom.getFullYear() + "-" + (uom.getMonth() + 1) + "-" + uom.getDate();
            return uom;
        }
    });
})(jQuery);