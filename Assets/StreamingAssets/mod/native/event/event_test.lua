EVENT_DEF.EVENT_TEST = 
{
    occur_rate = function ()
        if gm_data().date.day % 3 == 0 then
            return 0
        end
        return 0
    end,


    options = 
    {
        OPTION_1 = 
        {
            selected = function ()
                print("EVENT_TEST".."_OPTION_1_DESC selected")
            end
        }
    }
}