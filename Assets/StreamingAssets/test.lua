base_type=class()		-- ����һ������ base_type
 
function base_type:ctor(x)	-- ���� base_type �Ĺ��캯��
	print('base_type ctor')

    self.x = x
end


function base_type: print_x()-- ����һ����Ա���� base_type: print_x

    print(self.x)
end


function base_type: hello()-- ������һ����Ա���� base_type: hello

    print('hello base_type')
end


gmdata_type = class()

function gmdata_type:ctor(x)	-- ���� base_type �Ĺ��캯��
	print('gmdata_type ctor')
    self.x = x
end


function gmdata_type: print_x()-- ����һ����Ա���� base_type: print_x
    print(self.x)
end


function gmdata_type: hello()-- ������һ����Ա���� base_type: hello
    print('hello gmdata_type')
end

gmdata_type_caller =  class()

function gmdata_type_caller:ctor(x)	-- ���� base_type �Ĺ��캯��
	print('gmdata_type_caller ctor')
    x.hello()
end

SELECT_BACKGROUND = 
{
    area = 12,

    OPTION_1 = 
    {
        desc = function()
            return 'SELECT_BACKGROUND_OPT1'
        end
    },

    OPTION_2 = 
    {
        areasaa = 0,

        desc = function(this)
            this.areasaa = 10
            return 'SELECT_BACKGROUND_OPT2'
        end
    },

    desc = function (__self, arg1, arg2, arg3)
        return __self.area..""
    end
}



