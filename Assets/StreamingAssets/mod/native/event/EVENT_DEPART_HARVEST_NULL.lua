EVENT_DEF.DEPART.EVENT_DEPART_HARVEST_NULL = 
{
    options = 
    {
        OPTION_1 = 
        {
            selected = function ()
                gm_depart().buffers:find('HARVEST_NULL').exist = true
            end,
        }
    }
}